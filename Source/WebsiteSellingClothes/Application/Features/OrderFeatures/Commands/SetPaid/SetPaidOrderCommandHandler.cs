using Application.DTOs.Responses;
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

    public SetPaidOrderCommandHandler(IOrderRepository orderRepository, IPaymentRepository paymentRepository, ICartRepository cartRepository)
    {
        this.orderRepository = orderRepository;
        this.paymentRepository = paymentRepository;
        this.cartRepository = cartRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(SetPaidOrderCommand request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.PaymentId);
        if (payment == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        var order = await orderRepository.SetPaidAsync(payment.Order!.Id, payment);
        
        bool cartIsPaid = false;
        foreach (var cart in order!.Carts)
        {
            if (await cartRepository.SetPaidAsync(cart.Id) > 0) cartIsPaid = true;
            else cartIsPaid = false;
        }
        
        if (order !=null && cartIsPaid) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Setted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
