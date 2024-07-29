using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UploadImage;
public class UploadImageUserCommandValidator : AbstractValidator<UploadImageUserCommand>
{
    public UploadImageUserCommandValidator()
    {
        //RuleFor(x => x.UserId)
        //    .NotEmpty().WithMessage("The id can't empty")
        //    .NotNull().WithMessage("The id can't null");
        //RuleFor(x => x.UserImageRequestDto!.Image)
        //    .NotNull().WithMessage("The file is required");

    }
}
