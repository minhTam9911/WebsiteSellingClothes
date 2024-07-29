using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Queries.GetById;
public class GetByIdPaymentSignatureQueryHandler : IRequestHandler<GetByIdPaymentSignatureQuery, PaymentSignatureResponseDto>
{
    private readonly IMapper mapper;
    private readonly IPaymentSignatureRepository paymentSignatureRepository;

    public GetByIdPaymentSignatureQueryHandler(IMapper mapper, IPaymentSignatureRepository paymentSignatureRepository)
    {
        this.mapper = mapper;
        this.paymentSignatureRepository = paymentSignatureRepository;
    }

    public async Task<PaymentSignatureResponseDto> Handle(GetByIdPaymentSignatureQuery request, CancellationToken cancellationToken)
    {
        var paymentSignature = await paymentSignatureRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<PaymentSignatureResponseDto>(paymentSignature);
        return data;
    }
}
