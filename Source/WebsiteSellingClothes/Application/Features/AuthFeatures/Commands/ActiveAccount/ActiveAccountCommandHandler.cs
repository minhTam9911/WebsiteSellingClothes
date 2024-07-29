using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ActiveAccount;
public class ActiveAccountCommandHandler : IRequestHandler<ActiveAccountCommand, ServiceContainerResponseDto>
{

    private readonly IAuthRepository authRepository;

    public ActiveAccountCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(ActiveAccountCommand request, CancellationToken cancellationToken)
    {
        var result = await authRepository.ActiveAccountAsync(request.Email, request.Code);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Actived");
    }
}
