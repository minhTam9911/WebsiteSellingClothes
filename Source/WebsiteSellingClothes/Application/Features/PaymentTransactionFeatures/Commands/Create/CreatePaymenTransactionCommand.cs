using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Commands.Create;
public class CreatePaymenTransactionCommand : IRequest<DischargeWithDataResponseDto<PaymentTransactionResponseDto>>
{
    public PaymentTransactionRequestDto? PaymentTransactionRequestDto { get; set; }
}
