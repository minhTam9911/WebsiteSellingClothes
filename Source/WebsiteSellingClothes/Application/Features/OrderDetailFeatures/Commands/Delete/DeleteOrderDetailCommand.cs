using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Commands.Delete;
public class DeleteOrderDetailCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
}
