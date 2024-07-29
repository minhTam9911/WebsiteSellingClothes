using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.Delete;
public class DeletepaymentCommandValidator : AbstractValidator<DeletePaymentCommand>
{
    public DeletepaymentCommandValidator()
    {
    }
}
