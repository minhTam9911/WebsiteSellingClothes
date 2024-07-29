using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetByIdForMe;
public class GetByIdOrderDetailQuery : IRequest<OrderDetailResponseDto>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
}
