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

namespace Application.Features.OrderDetailFeatures.Queries.GetAllForMe;
public class GetAllForMeOrderDetailQueryHandler : IRequestHandler<GetAllForMeOrderDetailQuery, PagedListDto<OrderDetailResponseDto>>
{

    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IMapper mapper;

    public GetAllForMeOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
    {
        this.orderDetailRepository = orderDetailRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<OrderDetailResponseDto>> Handle(GetAllForMeOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await orderDetailRepository.GetAllForMeAsync(request.FilterDto!, request.UserId);
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
