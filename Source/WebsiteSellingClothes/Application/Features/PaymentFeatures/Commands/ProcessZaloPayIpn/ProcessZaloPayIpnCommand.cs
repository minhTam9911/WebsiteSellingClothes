using Application.DTOs.Responses;
using Application.DTOs.ZaloPays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.ProcessZaloPayIpn;
public class ProcessZaloPayIpnCommand : ZaloPayResultRequest, IRequest<ServiceContainerResponseDto>
{
}
