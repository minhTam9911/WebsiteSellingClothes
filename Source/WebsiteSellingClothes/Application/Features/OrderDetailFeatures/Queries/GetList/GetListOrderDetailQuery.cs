using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Queries.GetList;
public class GetListOrderDetailQuery : IRequest<PagedListDto<OrderDetailResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
