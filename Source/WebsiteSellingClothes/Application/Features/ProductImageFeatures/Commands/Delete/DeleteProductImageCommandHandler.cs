using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Commands.Delete;
public class DeleteProductImageCommandHandler : IRequestHandler<DeleteProductImageCommand, ServiceContainerResponseDto>
{
    private readonly IProductImageRepository productImageRepository;

    public DeleteProductImageCommandHandler(IProductImageRepository productImageRepository)
    {
        this.productImageRepository = productImageRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
    {
        var result = await productImageRepository.DeleteAsync(request.Id);
        if (result<=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
