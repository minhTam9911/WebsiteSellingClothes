using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ChangeForgotPassword;
public class ChangeForgotPasswordCommandHandler : IRequestHandler<ChangeForgotPasswordCommand, ServiceContainerResponseDto>
{
    private readonly IAuthRepository authRepository;

    public ChangeForgotPasswordCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(ChangeForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await authRepository.ChangeForgotPasswordAsync(request.ResetPasswordRequestDto!.Token!, request.ResetPasswordRequestDto.NewPassword!);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Changed");
    }
}
