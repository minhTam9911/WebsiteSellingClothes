using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Queries.GetAllForMe;
public class GetAllForMeFavouriteQueryHandler : IRequestHandler<GetAllForMeFavouriteQuery, PagedListDto<FavourireResponseDto>>
{

    private readonly IFavouriteRepository favouriteRepository;
    private readonly IMapper mapper;

    public GetAllForMeFavouriteQueryHandler(IFavouriteRepository favouriteRepository, IMapper mapper)
    {
        this.favouriteRepository = favouriteRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<FavourireResponseDto>> Handle(GetAllForMeFavouriteQuery request, CancellationToken cancellationToken)
    {
        var favourites = await favouriteRepository.GetAllForMeAsync(request.UserId, request.FilterDto!);
        var data = new PagedListDto<FavourireResponseDto>(){
            PageIndex = favourites!.PageIndex,
            PageSize = favourites.PageSize,
            TotalCount = favourites.TotalCount,
            Data = mapper.Map<List<FavourireResponseDto>>(favourites.Data)
        };
        return data;
    }
}
