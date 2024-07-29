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

namespace Application.Features.PaymentNotificationFeatures.Queries.GetList;
public class GetListPaymentNotificationQueryHandler : IRequestHandler<GetListPaymentNotificationQuery, PagedListDto<PaymentNotificationResponseDto>>
{
    private readonly IPaymentNotificationRepository paymentNotificationRepository;
    private readonly IMapper mapper;

    public GetListPaymentNotificationQueryHandler(IPaymentNotificationRepository paymentNotificationRepository, IMapper mapper)
    {
        this.paymentNotificationRepository = paymentNotificationRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<PaymentNotificationResponseDto>> Handle(GetListPaymentNotificationQuery request, CancellationToken cancellationToken)
    {
        var paymentNotifications = await paymentNotificationRepository.GetListAsync(request.FilterDto);
        var data = new PagedListDto<PaymentNotificationResponseDto>()
        {

            PageIndex = paymentNotifications.PageIndex,
            PageSize = paymentNotifications.PageSize,
            TotalCount = paymentNotifications.TotalCount,
            Data = mapper.Map<List<PaymentNotificationResponseDto>>(paymentNotifications.Data)

        };
        return data;
    }
}
