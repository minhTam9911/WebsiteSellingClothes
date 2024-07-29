using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.UploadImage;
public class UploadImageProductCommandValidator : AbstractValidator<UploadImageProductCommand>
{
    public UploadImageProductCommandValidator()
    {
        RuleFor(x => x.Images)
            .NotNull().WithMessage("The images is required ");
    }
}
