using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Create;
public class CreatePaymentNotificationCommandValidator : AbstractValidator<CreatePaymentNotificationCommand>
{
    public CreatePaymentNotificationCommandValidator()
    {
    }
}
