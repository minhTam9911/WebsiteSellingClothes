using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Queries.GetList;
public class GetListBrandQuery : IRequest<PagedListDto<BrandResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
