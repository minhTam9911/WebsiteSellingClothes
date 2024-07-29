using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Delete;
public class DeletePaymentNotificationCommandValidator : AbstractValidator<DeletePaymentNotificationCommand>
{
    public DeletePaymentNotificationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("The id is required");
    }
}
