using Application.DTOs.Responses;
using Application.Features.FavouriteFeatures.Queries.GetAll;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Queries.GetList;
public class GetListFavouriteQueryHandler : IRequestHandler<GetListFavouriteQuery, PagedListDto<FavourireResponseDto>>
{
    private readonly IFavouriteRepository favouriteRepository;
    private readonly IMapper mapper;

    public GetListFavouriteQueryHandler(IFavouriteRepository favouriteRepository, IMapper mapper)
    {
        this.favouriteRepository = favouriteRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<FavourireResponseDto>> Handle(GetListFavouriteQuery request, CancellationToken cancellationToken)
    {
        var favourites = await favouriteRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<FavourireResponseDto>()
        {
            PageIndex = favourites!.PageIndex,
            PageSize = favourites.PageSize,
            TotalCount = favourites.TotalCount,
            Data = mapper.Map<List<FavourireResponseDto>>(favourites.Data)
        };
        return data;
    }
}
