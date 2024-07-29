using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using MediatR;


namespace Application.Features.PaymentFeatures.Commands.ProcessVnPayIpn;
public class ProcessVnPayIpnCommand : VnPayResponseDto, IRequest<ServiceContainerResponseDto>
{
   
}