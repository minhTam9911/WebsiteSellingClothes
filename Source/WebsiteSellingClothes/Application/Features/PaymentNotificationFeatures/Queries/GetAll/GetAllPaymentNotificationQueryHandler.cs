using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Queries.GetAll;
public class GetAllPaymentNotificationQueryHandler : IRequestHandler<GetAllPaymentNotificationQuery, List<PaymentNotificationResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;

    public GetAllPaymentNotificationQueryHandler(IMapper mapper, IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.mapper = mapper;
        this.paymentNotificationRepository = paymentNotificationRepository;
    }

    public async Task<List<PaymentNotificationResponseDto>> Handle(GetAllPaymentNotificationQuery request, CancellationToken cancellationToken)
    {
        var paymentNotificaitons = await paymentNotificationRepository.GetAllAsync();
        var data = mapper.Map<List<PaymentNotificationResponseDto>>(paymentNotificaitons);
        return data;
    }
}
