using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetList;
public class GetListProductImageQuery : IRequest<PagedListDto<ProductImageResponseDto>>
{
    public FilterDto? FilterDto {  get; set; }
}
