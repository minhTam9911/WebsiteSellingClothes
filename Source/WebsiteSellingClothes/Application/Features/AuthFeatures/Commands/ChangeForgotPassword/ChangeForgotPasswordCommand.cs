using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ChangeForgotPassword;
public class ChangeForgotPasswordCommand : IRequest<ServiceContainerResponseDto>
{

    public ResetPasswordRequestDto? ResetPasswordRequestDto  { get; set; }

}
