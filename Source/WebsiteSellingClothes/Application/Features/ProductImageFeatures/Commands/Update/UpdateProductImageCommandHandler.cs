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

namespace Application.Features.ProductImageFeatures.Commands.Update;
public class UpdateProductImageCommandHandler : IRequestHandler<UpdateProductImageCommand,ServiceContainerResponseDto>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public UpdateProductImageCommandHandler(IProductImageRepository productImageRepository, IProductRepository productRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
    {
        var productImage = mapper.Map<ProductImage>(request.ProductImageRequestDto);
        productImage.Product = await productRepository.GetByIdAsync(request.ProductImageRequestDto!.ProductId);
        var result = await productImageRepository.UpdateAsync(request.Id, productImage);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");
    }
}
