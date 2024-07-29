using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AuthFeatures.Commands.ActiveAccount;
public class ActiveAccountCommand : IRequest<ServiceContainerResponseDto>
{
    public string Email { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;

}
