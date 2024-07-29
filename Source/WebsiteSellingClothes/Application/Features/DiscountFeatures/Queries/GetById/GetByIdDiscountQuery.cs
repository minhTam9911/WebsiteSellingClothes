using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Queries.GetById;
public class GetByIdDiscountQuery : IRequest<DiscountResponseDto>
{
    public string Id { get; set; } =  string.Empty;
}
