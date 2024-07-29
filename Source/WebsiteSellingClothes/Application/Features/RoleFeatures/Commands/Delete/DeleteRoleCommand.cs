using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleFeatures.Commands.Create;
public class DeleteRoleCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
}
