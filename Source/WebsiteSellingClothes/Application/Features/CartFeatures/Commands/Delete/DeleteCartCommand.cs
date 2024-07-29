using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Delete;
public class DeleteCartCommand : IRequest<ServiceContainerResponseDto>
{

    public int Id { get; set; }
    public Guid UserId { get; set; }

}
