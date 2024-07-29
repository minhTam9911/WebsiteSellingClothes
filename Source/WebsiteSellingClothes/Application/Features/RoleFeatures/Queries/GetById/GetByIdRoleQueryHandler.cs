using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Roles.Queries.GetAll;
public class GetByIdRoleQueryHandler : IRequestHandler<GetByIdRoleQuery, RoleResponseDto>
{
	private readonly IRoleRepository roleRepository;
	private readonly IMapper mapper;

	public GetByIdRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
	{
		this.roleRepository = roleRepository;
		this.mapper = mapper;
	}

	public async Task<RoleResponseDto> Handle(GetByIdRoleQuery request, CancellationToken cancellationToken)
	{
		var role = await roleRepository.GetByIdAsync(request.Id);
		var data = mapper.Map<RoleResponseDto>(role);
		return data;
	}
}
