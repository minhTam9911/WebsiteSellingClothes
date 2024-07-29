using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Queries.GetAll;
public class GetAllPaymentSignatureQueryHandler : IRequestHandler<GetAllPaymentSignatureQuery, List<PaymentSignatureResponseDto>>
{
    private readonly IPaymentSignatureRepository paymentSignatureRepository;
    private readonly IMapper mapper;

    public GetAllPaymentSignatureQueryHandler(IPaymentSignatureRepository paymentSignatureRepository, IMapper mapper)
    {
        this.paymentSignatureRepository = paymentSignatureRepository;
        this.mapper = mapper;
    }

    public async Task<List<PaymentSignatureResponseDto>> Handle(GetAllPaymentSignatureQuery request, CancellationToken cancellationToken)
    {
        var paymentSignatures = await paymentSignatureRepository.GetAllAsync();
        var data= mapper.Map<List<PaymentSignatureResponseDto>>(paymentSignatures);
        return data;
    }
}
