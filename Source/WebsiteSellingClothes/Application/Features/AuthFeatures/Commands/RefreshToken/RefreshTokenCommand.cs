using Application.DTOs.Requests;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.RefreshToken;
public class RefreshTokenCommand : IRequest<ApiDto>
{

    public RefreshTokenRequestDto? RefreshTokenRequestDto { get; set; }

}
