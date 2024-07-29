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

namespace Application.Features.PaymentTransactionFeatures.Queries.GetList;
public class GetListPaymentTransactionQueryHandler : IRequestHandler<GetListPaymentTransactionQuery, PagedListDto<PaymentTransactionResponseDto>>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IMapper mapper;

    public GetListPaymentTransactionQueryHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<PaymentTransactionResponseDto>> Handle(GetListPaymentTransactionQuery request, CancellationToken cancellationToken)
    {
        var paymentTransactions = await paymentTransactionRepository.GetListAsync(request.FilterDto);
        var data = new PagedListDto<PaymentTransactionResponseDto>()
        {
            Data = mapper.Map<List<PaymentTransactionResponseDto>>(paymentTransactions.Data),
            PageIndex = paymentTransactions.PageIndex,
            PageSize = paymentTransactions.PageSize,
            TotalCount = paymentTransactions.TotalCount
        };
        return data;
    }
}
