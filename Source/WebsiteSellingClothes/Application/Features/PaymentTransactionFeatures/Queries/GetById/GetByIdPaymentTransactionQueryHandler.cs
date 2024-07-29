using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Queries.GetById;
public class GetByIdPaymentTransactionQueryHandler : IRequestHandler<GetByIdPaymentTransactionQuery, PaymentTransactionResponseDto>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IMapper mapper;

    public GetByIdPaymentTransactionQueryHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<PaymentTransactionResponseDto> Handle(GetByIdPaymentTransactionQuery request, CancellationToken cancellationToken)
    {
        var paymentTransaction = await paymentTransactionRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<PaymentTransactionResponseDto>(paymentTransaction);
        return data;
    }
}
