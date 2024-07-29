using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Queries.GetById;
public class GetByIdBrandQuery : IRequest<BrandResponseDto>
{
    public int Id { get; set; } 
}
