using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Commands.Delete;
public class DeleteMerchantCommandValidator : AbstractValidator<DeleteMerchantCommand>
{
    public DeleteMerchantCommandValidator()
    {
    }
}
