using Application.DTOs.Responses;
using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Queries.GetAll;
public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, PagedListResponseDto<RoleResponseDto>>
{
	public readonly IRoleRepository roleRepository;
	public readonly IMapper mapper;

	public GetAllRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
	{
		this.roleRepository = roleRepository;
		this.mapper = mapper;
	}

	public async Task<PagedListResponseDto<RoleResponseDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
	{
		var roles = await roleRepository.GetListAsync(request.FilterRequestDto!);
		var data = new PagedListResponseDto<RoleResponseDto>
		{
			PageIndex = roles.PageIndex,
			PageSize = roles.PageSize,
			TotalCount = roles.TotalCount,
			Items = mapper.Map<List<RoleResponseDto>>(roles.Items)
		};
		return data;
	}
}
