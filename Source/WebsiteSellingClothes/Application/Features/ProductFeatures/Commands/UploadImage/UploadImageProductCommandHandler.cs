using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.UploadImage;
public class UploadImageProductCommandHandler : IRequestHandler<UploadImageProductCommand, ServiceContainerResponseDto>
{
    private readonly IProductRepository productRepository;

    public UploadImageProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(UploadImageProductCommand request, CancellationToken cancellationToken)
    {
        var result = await productRepository.UploadImageAsync(request.Id, request.Images!);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Uploaded");
    }
}
