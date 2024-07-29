using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.Create;
public class CreatePaymentCommand : IRequest<DischargeWithDataResponseDto<PaymentLinkDto>>
{
    public Guid UserId { get; set; }
    public Order? Order { get; set; }
    public PaymentRequestDto? PaymentRequestDto { get; set; }
}
