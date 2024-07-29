using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Commands.Delete;
public class DeleteContactCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
}
