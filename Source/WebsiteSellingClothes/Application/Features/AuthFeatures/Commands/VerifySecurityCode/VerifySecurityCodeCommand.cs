
using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;


namespace Application.Features.AuthFeatures.Commands.VerifySecurityCode;
public class VerifySecurityCodeCommand : IRequest<ServiceContainerResponseDto>
{
    public VerifySecurityCodeRequestDto? VerifySecurityCodeRequestDto { get; set; }
}
