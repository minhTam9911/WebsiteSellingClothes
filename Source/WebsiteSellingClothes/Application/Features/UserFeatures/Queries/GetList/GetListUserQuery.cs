using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetList;
public class GetListUserQuery : IRequest<PagedListDto<UserResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
