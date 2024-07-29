using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Create;
public class CreatePaymentNotificationCommand :IRequest<DischargeWithDataResponseDto<PaymentNotificationResponseDto>>
{
    public PaymentNotificationRequestDto? PaymentNotificationRequestDto { get; set; }
}
