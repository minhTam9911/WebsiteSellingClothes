using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands.SetPaid;
public class SetPaidOrderCommand : IRequest<ServiceContainerResponseDto>
{
    public string PaymentId { get; set; } = string.Empty;
    public decimal PaidAmount { get; set; }
}
