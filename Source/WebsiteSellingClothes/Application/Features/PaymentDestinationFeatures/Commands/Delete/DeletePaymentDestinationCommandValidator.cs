using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Delete;
public class DeletePaymentDestinationCommandValidator : AbstractValidator<DeletePaymentDestinationCommand>
{
    public DeletePaymentDestinationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotEmpty().WithMessage("The id is required");
    }
}
