using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Queries.GetById;
public class GetByIdPaymentSignatureQuery : IRequest<PaymentSignatureResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
