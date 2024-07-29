using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Queries.GetAllForMe;
public class GetAllForMeCartQuery : IRequest<PagedListDto<CartResponseDto>>
{
    public Guid UserId { get; set; }
    public FilterDto? FilterDto { get; set; }
}
