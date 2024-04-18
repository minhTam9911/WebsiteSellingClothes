using Application.DTOs.Responses;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Queries.GetAll;
public class GetAllRoleQuery : IRequest<PagedListResponseDto<RoleResponseDto>>
{
	public FilterRequestDto? FilterRequestDto { get; set; }
}
