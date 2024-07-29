using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Commands.Update;
public class UpdateProductImageCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public ProductImageRequestDto? ProductImageRequestDto { get; set; }
}
