using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetAllForMe;
public class GetAllForMeOrderQuery : IRequest<PagedListDto<OrderResponseDto>>
{
    public Guid UserId { get; set; }
    public FilterDto? FilterDto {  get; set; }
}
