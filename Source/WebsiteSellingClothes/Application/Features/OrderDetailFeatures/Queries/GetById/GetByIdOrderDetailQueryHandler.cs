using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetByIdForMe;
public class GetByIdOrderDetailQueryHandler : IRequestHandler<GetByIdOrderDetailQuery, OrderDetailResponseDto>
{
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IMapper mapper;

    public GetByIdOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
    {
        this.orderDetailRepository = orderDetailRepository;
        this.mapper = mapper;
    }

    public async Task<OrderDetailResponseDto> Handle(GetByIdOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetail = await orderDetailRepository.GetByIdAsync(request.Id, request.UserId);
        var data = mapper.Map<OrderDetailResponseDto>(orderDetail);
        return data;
    }
}
