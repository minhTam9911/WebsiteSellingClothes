using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetByCode;
public class GetByCodeProductQuery : IRequest<List<ProductResponseDto>>
{
    public string Code { get; set; } = string.Empty;
}
