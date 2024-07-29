using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleFeatures.Queries.GetList;
public class GetListRoleQuery : IRequest<PagedListDto<RoleResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
