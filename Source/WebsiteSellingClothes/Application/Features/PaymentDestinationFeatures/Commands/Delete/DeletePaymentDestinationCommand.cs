using Application.DTOs.Responses;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Delete;
public class DeletePaymentDestinationCommand : IRequest<ServiceContainerResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
