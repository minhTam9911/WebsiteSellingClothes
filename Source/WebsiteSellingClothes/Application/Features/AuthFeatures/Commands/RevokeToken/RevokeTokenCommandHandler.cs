using Application.DTOs.Responses;
using Azure.Core;
using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.RevokeToken;
public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand, ServiceContainerResponseDto>
{
    private readonly IAuthRepository authRepository;
  
    public RevokeTokenCommandHandler(IAuthRepository authRepository)
    {
        this.authRepository = authRepository;
       
    }

    public async Task<ServiceContainerResponseDto> Handle(RevokeTokenCommand request, CancellationToken cancellationToken)
    {
        var result = await authRepository.RevokeTokenAsync(request.UserId);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Revoked");
    }
}
