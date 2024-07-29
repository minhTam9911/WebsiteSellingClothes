using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetAllForMe;
public class GetAllForMeOrderDetailQuery : IRequest<PagedListDto<OrderDetailResponseDto>>
{
    public Guid UserId { get; set; }
    public FilterDto? FilterDto { get; set; }
}
