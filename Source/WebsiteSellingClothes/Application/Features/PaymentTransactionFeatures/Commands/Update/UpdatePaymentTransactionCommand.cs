using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Commands.Update;
public class UpdatePaymentTransactionCommand : IRequest<DischargeWithDataResponseDto<PaymentTransactionResponseDto>>
{
    public string Id { get; set; } = string.Empty;
    public PaymentTransactionRequestDto? PaymentTransactionRequestDto { get; set; }
}
