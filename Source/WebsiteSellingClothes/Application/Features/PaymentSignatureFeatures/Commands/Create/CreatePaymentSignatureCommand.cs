using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;

namespace Application.Features.PaymentSignatureFeatures.Commands.Create;
public class CreatePaymentSignatureCommand : IRequest<DischargeWithDataResponseDto<PaymentSignatureResponseDto>>
{
    public PaymentSignatureRequestDto? PaymentSignatureRequestDto { get; set; }
}
