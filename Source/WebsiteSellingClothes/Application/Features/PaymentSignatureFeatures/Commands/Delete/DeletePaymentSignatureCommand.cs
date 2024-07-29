using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Delete;
public class DeletePaymentSignatureCommand  : IRequest<ServiceContainerResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
