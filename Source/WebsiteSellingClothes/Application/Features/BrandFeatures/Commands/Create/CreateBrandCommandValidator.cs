using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Commands.Create;
public class CreateeBrandCommandValidator : AbstractValidator<CreateBrandCommand>
{
    public CreateeBrandCommandValidator()
    {
        RuleFor(x => x.BrandRequestDto!.Image)
            .NotNull().WithMessage("The image is required");
    }
}
