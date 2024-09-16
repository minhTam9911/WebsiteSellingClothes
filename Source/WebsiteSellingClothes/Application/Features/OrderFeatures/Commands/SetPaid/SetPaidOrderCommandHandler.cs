using Application.DTOs.Responses;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands.SetPaid;
public class SetPaidOrderCommandHandler : IRequestHandler<SetPaidOrderCommand, ServiceContainerResponseDto>
{
    private readonly IOrderRepository orderRepository;
    private readonly IPaymentRepository paymentRepository;
    private readonly ICartRepository cartRepository;
    private readonly IDiscountRepository discountRepository;
    private readonly IProductRepository productRepository;

    public SetPaidOrderCommandHandler(IOrderRepository orderRepository, IPaymentRepository paymentRepository, ICartRepository cartRepository, IDiscountRepository discountRepository, IProductRepository productRepository)
    {
        this.orderRepository = orderRepository;
        this.paymentRepository = paymentRepository;
        this.cartRepository = cartRepository;
        this.discountRepository = discountRepository;
        this.productRepository = productRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(SetPaidOrderCommand request, CancellationToken cancellationToken)
    {
        
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId);
        if (payment == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        var order = await orderRepository.SetPaidAsync(payment.Order!.Id, payment) ?? new Order();
        var discount = await discountRepository.GetByIdAsync(order.Discount!=null?order.Discount.Id:string.Empty);
        int totalQuantity = 0;
        bool cartIsPaid = false;
        foreach (var cart in order!.Carts)
        {
            if (await cartRepository.SetPaidAsync(cart.Id) > 0) cartIsPaid = true;
            else cartIsPaid = false;
        }
        foreach (var orderDetail in order.OrderDetails!)
        {
            if (discount != null)
            {
                var check = discount.Products!.Any(d => d.Id == orderDetail.Product!.Id);
                if (check) totalQuantity ++;
            }
            var product = await productRepository.GetByIdAsync(orderDetail.Product!.Id);
            product!.Quantity = product.Quantity - orderDetail.Quantity;
            await productRepository.UpdateAsync(product.Id, product);
        }
        if(totalQuantity > 0) await discountRepository.UpdateQuantityAsync(discount!.Id, totalQuantity);
        if (order !=null && cartIsPaid) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Setted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
