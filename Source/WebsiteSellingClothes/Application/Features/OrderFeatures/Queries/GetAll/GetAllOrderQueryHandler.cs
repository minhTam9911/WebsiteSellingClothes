using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetAll;
public class GetAllOrderQueryHandler : IRequestHandler<GetAllOrderQuery, List<OrderResponseDto>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetAllOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<List<OrderResponseDto>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var orders = await orderRepository.GetAllAsync();
            var data = mapper.Map<List<OrderResponseDto>>(orders);
            return data;
        }catch(Exception ex)
        {
            Debug.WriteLine(ex.Message);
            throw new Exception(ex.Message);
        }
        
        
    }
}
