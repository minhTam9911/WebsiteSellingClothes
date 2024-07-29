using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Create;
public class CreatePaymentDestinationCommandValidator : AbstractValidator<CreatePaymentDestinationCommand>
{
    public CreatePaymentDestinationCommandValidator()
    {
    }
}
