using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Delete;
public class DeleteCartCommandHandler : IRequestHandler<DeleteCartCommand, ServiceContainerResponseDto>
{
    private readonly ICartRepository cartRepository;

    public DeleteCartCommandHandler(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cartRepository.DeleteAsync(request.Id, request.UserId);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
