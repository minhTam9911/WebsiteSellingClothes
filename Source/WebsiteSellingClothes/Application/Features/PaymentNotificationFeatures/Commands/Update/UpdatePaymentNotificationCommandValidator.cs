using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Update;
public class UpdatePaymentNotificationCommandValidator : AbstractValidator<UpdatePaymentNotificationCommand>
{
    public UpdatePaymentNotificationCommandValidator()
    {
    }
}
