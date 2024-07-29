using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Commands.Create;
public class CreateFavouriteCommandHandler : IRequestHandler<CreateFavouriteCommand, ServiceContainerResponseDto>
{
    private readonly IFavouriteRepository favouriteRepository;

    public CreateFavouriteCommandHandler(IFavouriteRepository favouriteRepository)
    {
        this.favouriteRepository = favouriteRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateFavouriteCommand request, CancellationToken cancellationToken)
    {
        var result = await favouriteRepository.InsertAsync(request.FavouriteRequestDto!.ProductId, request.UserId);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
