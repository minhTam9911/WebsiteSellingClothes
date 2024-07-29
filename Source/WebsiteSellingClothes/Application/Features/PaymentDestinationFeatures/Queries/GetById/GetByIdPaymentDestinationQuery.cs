using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Queries.GetById;
public class GetByIdPaymentDestinationQuery : IRequest<PaymentDestinationResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
