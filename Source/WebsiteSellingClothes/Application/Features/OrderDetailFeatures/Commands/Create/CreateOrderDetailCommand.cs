using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Commands.Create;
public class CreateOrderDetailCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public OrderDetailRequestDto? OrderDetailRequestDto { get; set; }
}
