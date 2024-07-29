using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.Create;
public class CreateProductCommand : IRequest<ProductResponseDto?>
{
    public ProductRequestDto? ProductRequestDto { get; set; }
}
