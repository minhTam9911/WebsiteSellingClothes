using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetList;
public class GetListOrderDetailQueryHandler : IRequestHandler<GetListOrderDetailQuery, PagedListDto<OrderDetailResponseDto>>
{
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IMapper mapper;

    public GetListOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
    {
        this.orderDetailRepository = orderDetailRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<OrderDetailResponseDto>> Handle(GetListOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await orderDetailRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<OrderDetailResponseDto>()
        {
            PageIndex = orderDetails!.PageIndex,
            PageSize = orderDetails.PageSize,
            TotalCount = orderDetails.TotalCount,
            Data = mapper.Map<List<OrderDetailResponseDto>>(orderDetails.Data)
        };
        return data;
    }
}
