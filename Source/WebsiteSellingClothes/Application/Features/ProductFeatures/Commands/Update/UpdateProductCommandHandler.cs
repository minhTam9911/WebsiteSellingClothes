using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.Update;
public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceContainerResponseDto>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request.ProductRequestDto);
        var result = await productRepository.UpdateAsync(request.Id, product);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");
    }
}
