using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Commands.Create;
public class CreateDiscountCommand : IRequest<ServiceContainerResponseDto>
{
    public DiscountRequestDto? DiscountRequestDto { get; set; }
}
