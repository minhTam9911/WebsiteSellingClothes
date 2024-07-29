
using Application.DTOs.Responses;
using MediatR;


namespace Application.Features.AuthFeatures.Commands.RevokeToken;
public class RevokeTokenCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
}
