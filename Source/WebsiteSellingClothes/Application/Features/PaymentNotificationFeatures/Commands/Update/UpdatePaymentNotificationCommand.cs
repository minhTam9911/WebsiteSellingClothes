using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Update;
public class UpdatePaymentNotificationCommand : IRequest<DischargeWithDataResponseDto<PaymentNotificationResponseDto>>
{
    public string Id { get; set; } = string.Empty;
    public PaymentNotificationRequestDto? PaymentNotificationRequestDto { get; set; }
}
