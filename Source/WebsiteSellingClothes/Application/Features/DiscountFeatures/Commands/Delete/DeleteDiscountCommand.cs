using Application.DTOs.Responses;
using MediatR;


namespace Application.Features.DiscountFeatures.Commands.Delete;
public class DeleteDiscountCommand : IRequest<ServiceContainerResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
