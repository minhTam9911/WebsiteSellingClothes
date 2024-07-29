using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ForgotPassword;
public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ForgotPasswordResponseDto>
{

    private readonly IAuthRepository authRepository;

    public ForgotPasswordCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<ForgotPasswordResponseDto> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await authRepository.ForgotPasswordAsync(request.ForgotPasswordRequestDto!.Email);
        if (string.IsNullOrWhiteSpace(result)) return new ForgotPasswordResponseDto() {Status = (int)HttpStatusCode.InternalServerError,Flag= false,Token = string.Empty };
        return new ForgotPasswordResponseDto() { Status = (int)HttpStatusCode.OK, Flag = true, Token = result };
    }
}
