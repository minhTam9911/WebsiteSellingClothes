using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Delete;
public class DeletePaymentSignatureCommandValidator : AbstractValidator<DeletePaymentSignatureCommand>
{
    public DeletePaymentSignatureCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("The id is required");
    }
}
