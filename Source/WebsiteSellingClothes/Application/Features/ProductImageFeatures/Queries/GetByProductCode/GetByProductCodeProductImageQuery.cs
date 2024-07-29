using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetByProductCode;
public class GetByProductCodeProductImageQuery : IRequest<List<ProductImageResponseDto>>
{
    public string ProductCode { get; set; } = string.Empty;
}
