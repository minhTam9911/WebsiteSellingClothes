using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ForgotPassword;
public class ForgotPasswordCommand : IRequest<ForgotPasswordResponseDto>
{

    public ForgotPasswordRequestDto? ForgotPasswordRequestDto { get; set; }

}
