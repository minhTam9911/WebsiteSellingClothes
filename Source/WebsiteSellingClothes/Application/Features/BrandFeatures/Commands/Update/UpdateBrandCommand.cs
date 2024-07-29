using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Commands.Update;
public class UpdateBrandCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public BrandRequestDto? BrandRequestDto { get; set; }

}
