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

namespace Application.Features.PaymentSignatureFeatures.Queries.GetAllForMe;
public class GetAllForMePaymentSignatureQueryHandler : IRequestHandler<GetAllForMePaymentSignatureQuery,PagedListDto<PaymentSignatureResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentSignatureRepository paymentSignatureRepository;

    public GetAllForMePaymentSignatureQueryHandler(IMapper mapper, IPaymentSignatureRepository paymentSignatureRepository)
    {
        this.mapper = mapper;
        this.paymentSignatureRepository = paymentSignatureRepository;
    }

    public async Task<PagedListDto<PaymentSignatureResponseDto>> Handle(GetAllForMePaymentSignatureQuery request, CancellationToken cancellationToken)
    {
        var paymentSignatures = await paymentSignatureRepository.GetAllForMeAsync(request.UserId, request.FilterDto);
        var data = new PagedListDto<PaymentSignatureResponseDto>() { 
        
            PageIndex = paymentSignatures.PageIndex,
            PageSize = paymentSignatures.PageSize,
            TotalCount = paymentSignatures.TotalCount,
            Data = mapper.Map<List<PaymentSignatureResponseDto>>(paymentSignatures.Data)

        };
        return data;
    }
}
