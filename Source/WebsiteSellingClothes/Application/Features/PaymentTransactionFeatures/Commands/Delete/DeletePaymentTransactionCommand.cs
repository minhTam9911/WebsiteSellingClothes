using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Commands.Delete;
public class DeletePaymentTransactionCommand : IRequest<ServiceContainerResponseDto>
{
    public string Id { get; set; } = string.Empty; 
}
