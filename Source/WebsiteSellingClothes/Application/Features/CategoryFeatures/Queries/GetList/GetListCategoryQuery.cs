using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetList;
public class GetListCategoryQuery : IRequest<PagedListDto<CategoryResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
