using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetList;
public class GetListOrderQueryHandler : IRequestHandler<GetListOrderQuery, PagedListDto<OrderResponseDto>>
{
    private readonly IOrderRepository orderRepository;
    private readonly IMapper mapper;

    public GetListOrderQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        this.orderRepository = orderRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<OrderResponseDto>> Handle(GetListOrderQuery request, CancellationToken cancellationToken)
    {
        var orders = await orderRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<OrderResponseDto>()
        {
            PageIndex = orders!.PageIndex,
            PageSize = orders.PageSize,
            TotalCount = orders.TotalCount,
            Data = mapper.Map<List<OrderResponseDto>>(orders.Data)
        };
        return data;
    }
}
