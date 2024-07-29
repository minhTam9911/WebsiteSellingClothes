using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Queries.GetList;
public class GetListPaymentDestinationQueryHandler : IRequestHandler<GetListPaymentDestinationQuery, PagedListDto<PaymentDestinationResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentDestinationRepository paymentDestinationRepository;

    public GetListPaymentDestinationQueryHandler(IMapper mapper, IPaymentDestinationRepository paymentDestinationRepository)
    {
        this.mapper = mapper;
        this.paymentDestinationRepository = paymentDestinationRepository;
    }

    public async Task<PagedListDto<PaymentDestinationResponseDto>> Handle(GetListPaymentDestinationQuery request, CancellationToken cancellationToken)
    {
        var paymentDestinations = await paymentDestinationRepository.GetListAsync(request.FilterDto);
        var data = new PagedListDto<PaymentDestinationResponseDto>()
        {

            PageIndex = paymentDestinations.PageIndex,
            PageSize = paymentDestinations.PageSize,
            TotalCount = paymentDestinations.TotalCount,
            Data = mapper.Map<List<PaymentDestinationResponseDto>>(paymentDestinations.Data)

        };
        return data;
    }
}
