using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetAll;
public class GetAllOrderDetailQueryHandler : IRequestHandler<GetAllOrderDetailQuery, List<OrderDetailResponseDto>>
{
    private readonly IOrderDetailRepository orderDetailRepository;
    private readonly IMapper mapper;

    public GetAllOrderDetailQueryHandler(IOrderDetailRepository orderDetailRepository, IMapper mapper)
    {
        this.orderDetailRepository = orderDetailRepository;
        this.mapper = mapper;
    }

    public async Task<List<OrderDetailResponseDto>> Handle(GetAllOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var orderDetails = await orderDetailRepository.GetAllAsync();
        var data = mapper.Map<List<OrderDetailResponseDto>>(orderDetails);
        return data;
    }
}
