using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Queries.GetById;
public class GetByIdPaymentNotificationQuery : IRequest<PaymentNotificationResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
