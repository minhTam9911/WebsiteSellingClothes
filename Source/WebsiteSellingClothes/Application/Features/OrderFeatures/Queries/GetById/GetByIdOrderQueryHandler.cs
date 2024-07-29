using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetById;
public class GetByIdOrderQueryHandler : IRequestHandler<GetByIdOrderQuery, OrderResponseDto>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetByIdOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<OrderResponseDto> Handle(GetByIdOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await orderRepository.GetByIdAsync(request.Id,request.UserId);
        var data = mapper.Map<OrderResponseDto>(order);
        return data;
    }
}
