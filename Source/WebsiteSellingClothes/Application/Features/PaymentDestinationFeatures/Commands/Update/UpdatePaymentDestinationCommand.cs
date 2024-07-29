using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Update;
public class UpdatePaymentDestinationCommand : IRequest<DischargeWithDataResponseDto<PaymentDestinationResponseDto>>
{
    public string Id { get; set; } = string.Empty;
    public PaymentDestinationRequestDto? PaymentDestinationRequestDto { get; set; }
}
