using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Commands.Update;
public class UpdateCategoryCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public CategoryRequestDto? CategoryRequestDto { get; set; }

}
