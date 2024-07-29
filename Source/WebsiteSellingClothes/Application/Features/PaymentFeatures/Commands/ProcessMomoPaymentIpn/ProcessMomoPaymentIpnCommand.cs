using Application.DTOs.Momos;
using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.ProcessMomoPaymentIpn;
public class ProcessMomoPaymentIpnCommand : MomoOneTimePaymentResultRequest, IRequest<ServiceContainerResponseDto>
{
   
}
