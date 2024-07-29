﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Update;
public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{
    public UpdateCartCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThanOrEqualTo(1).WithMessage("The id must be between 1 and infinity");
    }
}
