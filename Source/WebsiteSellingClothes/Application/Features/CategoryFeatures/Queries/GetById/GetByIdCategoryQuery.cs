using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetById;
public class GetByIdCategoryQuery : IRequest<CategoryResponseDto>
{
    public int Id { get; set; } 
}
