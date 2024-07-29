using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ActiveAccount;
public class ActiveAccountCommandValidator : AbstractValidator<ActiveAccountCommand>
{
    public ActiveAccountCommandValidator()
    {

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email can't empty")
            .NotNull().WithMessage("The email can't null")
            .EmailAddress().WithMessage("The email invalid");
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("The email can't empty")
            .NotNull().WithMessage("The email can't null");

    }
}
