using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands.SetPaid;
public class SetPaidOrderCommandValidator : AbstractValidator<SetPaidOrderCommand>
{
    public SetPaidOrderCommandValidator()
    {
    }
}
