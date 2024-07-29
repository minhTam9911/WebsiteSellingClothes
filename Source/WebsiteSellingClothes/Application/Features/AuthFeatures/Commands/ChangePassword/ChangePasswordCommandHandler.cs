using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ChangePassword;
public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, ServiceContainerResponseDto>
{
    private readonly IAuthRepository authRepository;

    public ChangePasswordCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result = await authRepository.ChangePasswordAsync(request.UserId,request.ChangePasswordRequestDto!.CurrentPassword!,request.ChangePasswordRequestDto.NewPassword!);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Changed");
    }
}
