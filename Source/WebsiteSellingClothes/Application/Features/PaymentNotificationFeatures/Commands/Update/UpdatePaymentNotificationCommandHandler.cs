using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Update;
public class UpdatePaymentNotificationCommandHandler : IRequestHandler<UpdatePaymentNotificationCommand, DischargeWithDataResponseDto<PaymentNotificationResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;
    private readonly IMerchantRepository merchantRepository;
    private readonly IPaymentRepository paymentRepository;

    public UpdatePaymentNotificationCommandHandler(IMapper mapper, IPaymentNotificationRepository paymentNotificationRepository, IMerchantRepository merchantRepository, IPaymentRepository paymentRepository)
    {
        this.mapper = mapper;
        this.paymentNotificationRepository = paymentNotificationRepository;
        this.merchantRepository = merchantRepository;
        this.paymentRepository = paymentRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentNotificationResponseDto>> Handle(UpdatePaymentNotificationCommand request, CancellationToken cancellationToken)
    {
        var paymentNotification = mapper.Map<PaymentNotification>(request.PaymentNotificationRequestDto);
        paymentNotification.Merchant = await merchantRepository.GetByIdAsync(request.PaymentNotificationRequestDto!.MerchantId);
        paymentNotification.Payment = await paymentRepository.GetByIdAsync(request.PaymentNotificationRequestDto.PaymentId);
        paymentNotification.Merchant = await merchantRepository.GetByIdAsync(request.PaymentNotificationRequestDto.MerchantId);
        var result = await paymentNotificationRepository.UpdateAsync(request.Id, paymentNotification);
        if (result == null)
        {
            var data = new DischargeWithDataResponseDto<PaymentNotificationResponseDto>()
            {

                Flag = false,
                Message = "Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Data = null,

            };
            return data;
        }
        else
        {
            var data = new DischargeWithDataResponseDto<PaymentNotificationResponseDto>()
            {

                Flag = true,
                Message = "Inserted",
                Status = (int)HttpStatusCode.OK,
                Data = mapper.Map<PaymentNotificationResponseDto>(result)

            };
            return data;
        }
    }
}
