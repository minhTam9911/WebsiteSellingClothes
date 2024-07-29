using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Commands.Delete;
public class DeleteFavouriteCommandHandler : IRequestHandler<DeleteFavouriteCommand, ServiceContainerResponseDto>
{
    private readonly IFavouriteRepository favouriteRepository;

    public DeleteFavouriteCommandHandler(IFavouriteRepository favouriteRepository)
    {
        this.favouriteRepository = favouriteRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteFavouriteCommand request, CancellationToken cancellationToken)
    {
        var result = await favouriteRepository.DeleteAsync(request.Id, request.UserId);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
