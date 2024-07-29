using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Update;
public class UpdatePaymentSignatureCommand : IRequest<DischargeWithDataResponseDto<PaymentSignatureResponseDto>>
{
    public string Id { get; set; } = string.Empty;
    public PaymentSignatureRequestDto? PaymentSignatureRequestDto { get; set; }
}
