using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetAllActive;
public class GetAllActiveProductQuery : IRequest<PagedListDto<ProductResponseDto>>
{
    public bool IsActive { get; set; }
    public FilterDto? FilterDto { get; set; }
}
