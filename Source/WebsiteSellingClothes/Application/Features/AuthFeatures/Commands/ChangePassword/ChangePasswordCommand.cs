using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ChangePassword;
public class ChangePasswordCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public ChangePasswordRequestDto? ChangePasswordRequestDto { get; set; }

}
