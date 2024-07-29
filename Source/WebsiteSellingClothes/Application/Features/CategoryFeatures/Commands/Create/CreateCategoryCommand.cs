using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;

namespace Application.Features.CategoryFeatures.Commands.Create;
public class CreateCategoryCommand : IRequest<ServiceContainerResponseDto>
{

    public CategoryRequestDto? CategoryRequestDto { get; set; }

}
