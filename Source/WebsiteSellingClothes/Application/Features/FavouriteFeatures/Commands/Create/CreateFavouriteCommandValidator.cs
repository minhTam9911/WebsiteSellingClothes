using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Commands.Create;
public class CreateFavouriteCommandValidator : AbstractValidator<CreateFavouriteCommand>
{
    public CreateFavouriteCommandValidator()
    {
    }
}
