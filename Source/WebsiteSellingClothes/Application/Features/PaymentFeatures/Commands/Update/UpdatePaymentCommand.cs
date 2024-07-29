using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.Update;
public class UpdatePaymentCommand : IRequest<DischargeWithDataResponseDto<PaymentLinkDto>>
{
    public string Id { get; set; } = string.Empty;
    public PaymentRequestDto PaymentRequestDto { get; set; } = new PaymentRequestDto();
}
