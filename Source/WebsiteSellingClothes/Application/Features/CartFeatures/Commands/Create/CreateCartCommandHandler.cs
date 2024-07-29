using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Create;
public class CreateCartCommandHandler : IRequestHandler<CreateCartCommand, ServiceContainerResponseDto>
{

    private readonly ICartRepository cartRepository;

    public CreateCartCommandHandler(ICartRepository cartRepository)
    {
        this.cartRepository = cartRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateCartCommand request, CancellationToken cancellationToken)
    {
        var result = await cartRepository.InsertAsync(request.CartRequestDto!.ProductId, request.CartRequestDto.Quantity, request.UserId);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
