using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands.Create;
public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Order?>
{
    private readonly IOrderRepository orderRepository;
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;
    private readonly ICartRepository cartRepository;
    private readonly IProductRepository productRepository;
    private readonly IDiscountRepository discountRepository;
    private readonly IOrderDetailRepository orderDetailRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository, IUserRepository userRepository, IMapper mapper, IDiscountRepository discountRepository, ICartRepository cartRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
    {
        this.orderRepository = orderRepository;
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.discountRepository = discountRepository;
        this.cartRepository = cartRepository;
        this.productRepository = productRepository;
        this.orderDetailRepository = orderDetailRepository;
    }

    public async Task<Order?> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = mapper.Map<Order>(request.OrderRequestDto);
        order.User = await userRepository.GetByIdAsync(request.UserId);
        var discount = await discountRepository.GetByIdAsync(request.OrderRequestDto!.DiscountId!);
        var quantityProduct = 0;
        decimal totalAmount = 0;
        foreach (var item in request.OrderRequestDto!.CartIds!)
        {
            var cart = await cartRepository.GetByIdAsync(item, request.UserId);
            order.Carts.Add(cart!);
            quantityProduct += cart!.Quantity;
            var amount = cart.Product!.Price * cart.Quantity;
            if (discount != null)
            {
                if (discount!.EndDate > DateTime.Now && discount.Quantity > 0)
                {
                    if (discount.Products!.Where(x => x.Id == cart.Product.Id && x.Quantity >= cart.Quantity).Any())
                    {
                        amount = amount - amount * discount.Percentage / 100;
                    }

                }
            }

            totalAmount += amount;
        }
        order.Quantity = quantityProduct;
        order.Amount = totalAmount;
        order.Status = "Unpaid";
        order.Discount = discount;
        var result = await orderRepository.InsertAsync(order);
        if (result == null) return null;
        foreach (var item in request.OrderRequestDto!.CartIds!)
        {
            var cart = await cartRepository.GetByIdAsync(item, request.UserId);
            var amount = cart!.Product!.Price * cart.Quantity;
            if (discount != null)
            {
                if (discount!.EndDate > DateTime.Now && discount.Quantity > 0)
                {
                    if (discount.Products!.Where(x => x.Id == cart.Product.Id && x.Quantity >= cart.Quantity).Any())
                    {
                        amount = amount - amount / discount.Percentage;
                    }

                }
            }

            var orderDetail = new OrderDetail()
            {
                Order = result,
                Product = cart!.Product,
                Price = cart.Product.Price,
                Quantity = cart.Quantity,
                TotalAmount = amount,
                User = result.User
            };
            await orderDetailRepository.InsertAsync(orderDetail);

        }
        if (discount != null)
        {
            result.Discount!.Id = request.OrderRequestDto.DiscountId!;
        }
       
        return result;
    }
}
