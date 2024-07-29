using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Update;
public class UpdateCartCommandHandler : IRequestHandler<UpdateCartCommand, ServiceContainerResponseDto>
{
    private readonly ICartRepository cartRepository;

    public UpdateCartCommandHandler(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cartRepository.UpdateAsync(request.Id, request.CartRequestDto.ProductId, request.CartRequestDto.Quantity, request.UserId);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");
    }
}
