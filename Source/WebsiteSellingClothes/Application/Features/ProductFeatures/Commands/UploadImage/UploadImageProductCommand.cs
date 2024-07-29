using Application.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.UploadImage;
public class UploadImageProductCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public IFormFile[]? Images { get; set; }
}
