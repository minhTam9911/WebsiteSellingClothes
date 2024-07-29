using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Commands.Update;
public class UpdateCartCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public int Id { get; set; }
    public CartRequestDto? CartRequestDto { get; set; }
}
