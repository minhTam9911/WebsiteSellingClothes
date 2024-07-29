using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Commands.Delete;
public class DeletePaymentTransactionCommandValidator : AbstractValidator<DeletePaymentTransactionCommand>
{
    public DeletePaymentTransactionCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("The id is required");
    }

}
