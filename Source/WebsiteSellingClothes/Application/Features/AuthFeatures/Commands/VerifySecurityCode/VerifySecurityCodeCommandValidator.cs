using Application.Features.AuthFeatures.Commands.RevokeToken;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.VerifySecurityCode;
public class VerifySecurityCodeCommandValidator : AbstractValidator<VerifySecurityCodeCommand>
{
    public VerifySecurityCodeCommandValidator()
    {
    }
}
