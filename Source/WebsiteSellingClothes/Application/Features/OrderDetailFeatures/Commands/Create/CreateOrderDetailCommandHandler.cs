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

namespace Application.Features.OrderDetailFeatures.Commands.Create;
public class CreateOrderDetailCommandHandler : IRequestHandler<CreateOrderDetailCommand, ServiceContainerResponseDto>
{
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IProductRepository productRepository;
    private readonly IUserRepository userRepository;
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public CreateOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, IUserRepository userRepository, IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderDetailRepository = orderDetailRepository;
        this.productRepository = productRepository;
        this.userRepository = userRepository;
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var orderDetail = mapper.Map<OrderDetail>(request.OrderDetailRequestDto);
        orderDetail.Product = await productRepository.GetByIdAsync(request.OrderDetailRequestDto!.ProductId);
        orderDetail.Order = await orderRepository.GetByIdAsync(request.OrderDetailRequestDto.OrderId,request.UserId);
        orderDetail.User = await userRepository.GetByIdAsync(request.UserId);
        var result = await orderDetailRepository.InsertAsync(orderDetail);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
