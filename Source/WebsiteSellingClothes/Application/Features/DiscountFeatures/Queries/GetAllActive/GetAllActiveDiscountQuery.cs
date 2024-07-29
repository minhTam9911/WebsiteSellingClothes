using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Queries.GetAllActive;
public class GetAllActiveDiscountQuery : IRequest<PagedListDto<DiscountResponseDto>>
{
    public bool IsActive { get; set; }
    public int PageSize { get; set; }
    public int PageIndex { get; set; }
}
