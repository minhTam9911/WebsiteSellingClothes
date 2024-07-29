using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.RefreshToken;
public class RevokeTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RevokeTokenCommandValidator()
    {
    }
}
