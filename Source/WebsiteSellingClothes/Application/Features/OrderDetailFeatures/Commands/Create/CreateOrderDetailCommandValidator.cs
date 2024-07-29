using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Commands.Create;
public class CreateOrderDetailCommandValidator : AbstractValidator<CreateOrderDetailCommand>
{
    public CreateOrderDetailCommandValidator()
    {
    }
}
