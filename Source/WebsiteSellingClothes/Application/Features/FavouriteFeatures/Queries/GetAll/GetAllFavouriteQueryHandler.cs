using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Queries.GetAll;
public class GetAllFavouriteQueryHandler : IRequestHandler<GetAllFavouriteQuery, List<FavourireResponseDto>>
{
    private readonly IFavouriteRepository favouriteRepository;
    private readonly IMapper mapper;

    public GetAllFavouriteQueryHandler(IFavouriteRepository favouriteRepository, IMapper mapper)
    {
        this.favouriteRepository = favouriteRepository;
        this.mapper = mapper;
    }

    public async Task<List<FavourireResponseDto>> Handle(GetAllFavouriteQuery request, CancellationToken cancellationToken)
    {
        var favourites = await favouriteRepository.GetAllAsync();
        var data = mapper.Map<List<FavourireResponseDto>>(favourites);
        return data;
    }
}
