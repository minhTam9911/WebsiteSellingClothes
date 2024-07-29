using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.Delete;
public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ServiceContainerResponseDto>
{
    private readonly IProductRepository productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await productRepository.DeleteAsync(request.Id);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
