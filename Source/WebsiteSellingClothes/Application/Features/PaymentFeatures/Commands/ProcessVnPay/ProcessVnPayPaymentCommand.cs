using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.ProcessVnPay;
public class ProcessVnPayPaymentCommand : VnPayResponseDto, IRequest<DischargeWithDataResponseDto<(PaymentReturnDto, string)>>
{
   
}
