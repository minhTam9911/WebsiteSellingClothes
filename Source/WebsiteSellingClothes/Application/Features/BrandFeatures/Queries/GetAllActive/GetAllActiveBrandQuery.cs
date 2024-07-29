using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Queries.GetAllActive;
public class GetAllActiveBrandQuery : IRequest<PagedListDto<BrandResponseDto>>
{
    public bool IsActive { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
