using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;

namespace Application.Features.CartFeatures.Commands.Create;
public class CreateCartCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public CartRequestDto? CartRequestDto { get; set; }

}
