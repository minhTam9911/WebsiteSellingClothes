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

namespace Application.Features.PaymentTransactionFeatures.Queries.GetAllForMe;
public class GetAllForMePaymentTransactionQueryHandler : IRequestHandler<GetAllForMePaymentTransactionQuery, PagedListDto<PaymentTransactionResponseDto>>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IMapper mapper;

    public GetAllForMePaymentTransactionQueryHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<PaymentTransactionResponseDto>> Handle(GetAllForMePaymentTransactionQuery request, CancellationToken cancellationToken)
    {
        var paymentTransactions = await paymentTransactionRepository.GetAllForMeAsync(request.UserId,request.FilterDto);
        var data = new PagedListDto<PaymentTransactionResponseDto>() { 
        
            PageIndex = paymentTransactions.PageIndex,
            PageSize = paymentTransactions.PageSize,
            TotalCount = paymentTransactions.TotalCount,
            Data = mapper.Map<List<PaymentTransactionResponseDto>>(paymentTransactions.Data)

        };
        return data;
    }
}
