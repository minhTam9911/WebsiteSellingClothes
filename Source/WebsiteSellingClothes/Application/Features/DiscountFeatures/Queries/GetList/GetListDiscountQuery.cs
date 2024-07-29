using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Queries.GetList;
public class GetListDiscountQuery : IRequest<PagedListDto<DiscountResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
