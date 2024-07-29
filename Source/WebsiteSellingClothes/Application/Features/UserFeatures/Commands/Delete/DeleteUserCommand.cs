using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.Delete;
public class DeleteUserCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
}
