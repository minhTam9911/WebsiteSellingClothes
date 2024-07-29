using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Commands.Create;
public class CreateContactCommand : IRequest<ServiceContainerResponseDto>
{
    public ContactRequestDto? ContactRequestDto { get; set; }
}
