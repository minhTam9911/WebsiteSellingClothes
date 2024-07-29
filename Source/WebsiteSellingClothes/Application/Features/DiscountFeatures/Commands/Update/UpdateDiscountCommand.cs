using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Commands.Update;
public class UpdateDiscountCommand : IRequest<ServiceContainerResponseDto>
{
    public string Id { get; set; } = string.Empty;
    public DiscountRequestDto? DiscountRequestDto { get; set; }
}
