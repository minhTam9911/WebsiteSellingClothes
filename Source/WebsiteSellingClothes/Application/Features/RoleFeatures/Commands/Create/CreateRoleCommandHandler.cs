using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleFeatures.Commands.Create;
public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, ServiceContainerResponseDto>
{
	public readonly IRoleRepository roleRepository;
	public readonly IMapper mapper;

	public CreateRoleCommandHandler(IRoleRepository roleRepository, IMapper mapper)
	{
		this.roleRepository = roleRepository;
		this.mapper = mapper;
	}
	public async Task<ServiceContainerResponseDto> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
	{
		var role = mapper.Map<Role>(request.RoleRequestDto);
		var result = await roleRepository.InsertAsync(role);
		if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError,false, "Error");
		return new ServiceContainerResponseDto((int)HttpStatusCode.OK,true,"Inserted");
	}
}
