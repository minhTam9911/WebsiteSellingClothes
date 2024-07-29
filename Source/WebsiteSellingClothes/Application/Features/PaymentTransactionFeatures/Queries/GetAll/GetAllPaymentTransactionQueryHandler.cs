using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Queries.GetAll;
public class GetAllPaymentTransactionQueryHandler : IRequestHandler<GetAllPaymentTransactionQuery, List<PaymentTransactionResponseDto>>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IMapper mapper;

    public GetAllPaymentTransactionQueryHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<List<PaymentTransactionResponseDto>> Handle(GetAllPaymentTransactionQuery request, CancellationToken cancellationToken)
    {
        var paymentTransactions = await paymentTransactionRepository.GetAllAsync();
        var data = mapper.Map<List<PaymentTransactionResponseDto>>(paymentTransactions);
        return data;
    }
}
