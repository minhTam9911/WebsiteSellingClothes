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

namespace Application.Features.PaymentNotificationFeatures.Queries.GetAllForMe;
public class GetAllForMePaymentNotificationQueryHandler : IRequestHandler<GetAllForMePaymentNotificationQuery, PagedListDto<PaymentNotificationResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;

    public GetAllForMePaymentNotificationQueryHandler(IMapper mapper, IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.mapper = mapper;
        this.paymentNotificationRepository = paymentNotificationRepository;
    }

    public async Task<PagedListDto<PaymentNotificationResponseDto>> Handle(GetAllForMePaymentNotificationQuery request, CancellationToken cancellationToken)
    {
        var paymentNotifications = await paymentNotificationRepository.GetAllForMeAsync(request.UserId, request.FilterDto);
        var data = new PagedListDto<PaymentNotificationResponseDto>() {

            PageIndex = paymentNotifications.PageIndex,
            PageSize = paymentNotifications.PageSize,
            TotalCount = paymentNotifications.TotalCount,
            Data = mapper.Map<List<PaymentNotificationResponseDto>>(paymentNotifications.Data)

        };
        return data;
    }
}
