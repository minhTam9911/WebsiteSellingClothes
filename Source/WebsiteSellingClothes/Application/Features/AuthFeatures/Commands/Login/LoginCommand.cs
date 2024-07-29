using Application.DTOs.Requests;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Login;
public class LoginCommand : IRequest<ApiDto>
{
    public LoginRequestDto? LoginRequestDto { get; set; }
}
