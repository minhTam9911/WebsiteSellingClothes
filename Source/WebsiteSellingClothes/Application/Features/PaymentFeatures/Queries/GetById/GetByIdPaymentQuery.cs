using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Queries.GetById;
public class GetByIdPaymentQuery : IRequest<DischargeWithDataResponseDto<PaymentResponseDto>>
{
    public string Id { get; set; } = string.Empty;
}
