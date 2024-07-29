using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetById;
public class GetByIdProductQuery : IRequest<ProductResponseDto>
{
    public int Id { get; set; }
}
