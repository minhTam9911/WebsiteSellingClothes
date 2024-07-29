using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using Application.DTOs.ZaloPays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.ProcessZaloPay;
public class ProcessZaloPayCommand : ZaloPayResultRequest, IRequest<DischargeWithDataResponseDto<(PaymentReturnDto, string)>>
{
}
