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

namespace Application.Features.PaymentFeatures.Queries.GetAllForMe;
public class GetAllPaymentQueryHandler : IRequestHandler<GetAllForMePaymentQuery, PagedListDto<PaymentResponseDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetAllPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<PaymentResponseDto>> Handle(GetAllForMePaymentQuery request, CancellationToken cancellationToken)
    {
        var payments = await paymentRepository.GetAllForMeAsync(request.UserId, request.FilterDto);
        var data = new PagedListDto<PaymentResponseDto>()
        {
            Data = mapper.Map<List<PaymentResponseDto>>(payments.Data),
            PageIndex = payments.PageIndex,
            PageSize = payments.PageSize,
            TotalCount = payments.TotalCount
        };
        return data;
    }
}
