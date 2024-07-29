using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Commands.Create;
public class CreateProductImageCommand : IRequest<ServiceContainerResponseDto>
{
    public ProductImageRequestDto? ProductImageRequestDto {  get; set; }
}
