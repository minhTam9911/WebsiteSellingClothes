using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.RoleFeatures.Commands.Create;
public class UpdateRoleCommandValidation : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidation()
    {
    }
}
