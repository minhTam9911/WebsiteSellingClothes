using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Update;
public class UpdatePaymentDestinationCommandValidator : AbstractValidator<UpdatePaymentDestinationCommand>
{
    public UpdatePaymentDestinationCommandValidator()
    {
    }
}
