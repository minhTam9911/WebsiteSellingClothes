using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleFeatures.Commands.Create;
public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, ServiceContainerResponseDto>
{
    private readonly IRoleRepository roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await roleRepository.DeleteAsync(request.Id);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
