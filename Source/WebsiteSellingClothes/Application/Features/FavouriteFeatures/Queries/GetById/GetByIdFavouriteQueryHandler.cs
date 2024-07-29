using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Queries.GetById;
public class GetByIdFavouriteQueryHandler : IRequestHandler<GetByIdFavouriteQuery, FavourireResponseDto>
{
    private readonly IFavouriteRepository favouriteRepository;
    private readonly IMapper mapper;

    public GetByIdFavouriteQueryHandler(IFavouriteRepository favouriteRepository, IMapper mapper)
    {
        this.favouriteRepository = favouriteRepository;
        this.mapper = mapper;
    }

    public async Task<FavourireResponseDto> Handle(GetByIdFavouriteQuery request, CancellationToken cancellationToken)
    {
        var favourite = await favouriteRepository.GetByIdAsync(request.Id,request.UserId);
        var data = mapper.Map<FavourireResponseDto>(favourite);
        return data;
    }
}
