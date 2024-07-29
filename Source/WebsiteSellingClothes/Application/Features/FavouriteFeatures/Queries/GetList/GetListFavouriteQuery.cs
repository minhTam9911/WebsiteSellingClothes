using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Queries.GetList;
public class GetListFavouriteQuery : IRequest<PagedListDto<FavourireResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
