using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.Roles.Queries.GetAll;
public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleResponseDto>>
{
	public readonly IRoleRepository roleRepository;
	public readonly IMapper mapper;

	public GetAllRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
	{
		this.roleRepository = roleRepository;
		this.mapper = mapper;
	}

	public async Task<List<RoleResponseDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
	{
		var roles = await roleRepository.GetAllAsync();
		var data = mapper.Map<List<RoleResponseDto>>(roles);
		return data;
	}
}
