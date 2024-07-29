using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Queries.GetById;
public class GetByIdPaymentNotificationQueryHandler : IRequestHandler<GetByIdPaymentNotificationQuery, PaymentNotificationResponseDto>
{
    private readonly IMapper mapper;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;

    public GetByIdPaymentNotificationQueryHandler(IMapper mapper, IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.mapper = mapper;
        this.paymentNotificationRepository = paymentNotificationRepository;
    }

    public async Task<PaymentNotificationResponseDto> Handle(GetByIdPaymentNotificationQuery request, CancellationToken cancellationToken)
    {
        var paymentNotification = await paymentNotificationRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<PaymentNotificationResponseDto>(paymentNotification);
        return data;
    }
}
