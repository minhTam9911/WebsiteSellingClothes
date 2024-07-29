using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Queries.GetById;
public class GetByIdPaymentTransactionQuery : IRequest<PaymentTransactionResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
