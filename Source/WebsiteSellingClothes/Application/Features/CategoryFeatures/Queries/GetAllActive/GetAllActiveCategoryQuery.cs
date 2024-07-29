using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetAllActive;
public class GetAllActiveCategoryQuery : IRequest<PagedListDto<CategoryResponseDto>>
{
    public bool IsActive { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
