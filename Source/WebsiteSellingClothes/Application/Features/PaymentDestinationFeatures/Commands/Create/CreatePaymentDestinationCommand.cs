using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Create;
public class CreatePaymentDestinationCommand : IRequest<DischargeWithDataResponseDto<PaymentDestinationResponseDto>>
{
    public PaymentDestinationRequestDto? PaymentDestinationRequestDto { get; set; }
}
