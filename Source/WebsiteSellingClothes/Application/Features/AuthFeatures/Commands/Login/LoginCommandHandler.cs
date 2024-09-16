using Application.Commons.Exceptions;
using Application.DTOs.Responses;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.Login;
public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiDto>
{
    private readonly IAuthRepository authRepository;
    public LoginCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<ApiDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {

        var data = await authRepository.LoginAsync(request.LoginRequestDto!.Username,request.LoginRequestDto.Password);
        if (data == null) return new ApiDto((int)HttpStatusCode.InternalServerError,false, string.Empty,string.Empty, 0 ,string.Empty);
        return data;
    }
}
