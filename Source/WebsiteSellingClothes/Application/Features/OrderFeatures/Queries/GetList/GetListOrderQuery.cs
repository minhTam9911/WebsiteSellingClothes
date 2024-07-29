using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Queries.GetList;
public class GetListOrderQuery : IRequest<PagedListDto<OrderResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
