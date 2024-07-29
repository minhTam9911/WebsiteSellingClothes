using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetById;
public class GetByIdOrderQuery : IRequest<OrderResponseDto>
{
    public string Id { get; set; }
    public Guid UserId { get; set; }
}
