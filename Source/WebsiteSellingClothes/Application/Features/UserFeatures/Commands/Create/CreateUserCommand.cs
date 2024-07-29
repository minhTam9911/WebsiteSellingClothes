using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.Create;
public class CreateUserCommand : IRequest<ServiceContainerResponseDto>
{
    public UserRequestDto? UserRequestDto { get; set; }
}
