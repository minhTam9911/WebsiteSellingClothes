using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Create;
public class CreatePaymentSignatureCommandValidator : AbstractValidator<CreatePaymentSignatureCommand>
{
    public CreatePaymentSignatureCommandValidator()
    {
    }
}
