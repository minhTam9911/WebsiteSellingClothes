using Application.Commons.Exceptions;
using Application.DTOs.Responses;
using Azure.Core;
using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.RefreshToken;
public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, ApiDto?>
{
    private readonly IAuthRepository authRepository;
    private readonly IConfiguration configuration;

    public RefreshTokenCommandHandler(IAuthRepository authRepository, IConfiguration configuration)
    {
        this.authRepository = authRepository;
        this.configuration = configuration;
    }

    public async Task<ApiDto?> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        request.RefreshTokenRequestDto!.AccessToken = request.RefreshTokenRequestDto!.AccessToken!.Replace("Bearer", "");
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jwtDecode = new JwtSecurityToken();

        jwtDecode = handler.ReadJwtToken(request.RefreshTokenRequestDto.AccessToken);
        var timestamp = jwtDecode.Claims.FirstOrDefault(x => x.Type == "exp");
        long claim = long.Parse(timestamp!.Value);
        var exp = DateTime.UnixEpoch.AddHours(7).AddSeconds(claim);
        if (exp <= DateTime.Now.AddMinutes(5))
        {
            var data = await authRepository.RefreshTokenAsync(request.RefreshTokenRequestDto!.RefreshToken);
            return data;
        }
        return new ApiDto((int)HttpStatusCode.OK, true, "Bearer", request.RefreshTokenRequestDto!.AccessToken, int.Parse(configuration["AppSettings:ExpiresJwt"]!) * 60, request.RefreshTokenRequestDto!.RefreshToken);
        //var claim = jwtDecode!.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp);

    }
}
