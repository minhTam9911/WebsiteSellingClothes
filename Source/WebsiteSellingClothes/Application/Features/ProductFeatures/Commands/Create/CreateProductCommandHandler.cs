using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.Create;
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductResponseDto?>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;
    private readonly ICategoryRepository categoryRepository;
    private readonly IBrandRepository brandRepository;

    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper, IBrandRepository brandRepository, ICategoryRepository categoryRepository)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
        this.brandRepository = brandRepository;
        this.categoryRepository = categoryRepository;
    }

    public async Task<ProductResponseDto?> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request.ProductRequestDto);
        product.Brand = await brandRepository.GetByIdAsync(request.ProductRequestDto!.BrandId);
        product.Category = await categoryRepository.GetByIdAsync(request.ProductRequestDto.CategoryId);
        var result = await productRepository.InsertAsync(product,request.ProductRequestDto!.Images);
        if (result == null) return null;
        return mapper.Map<ProductResponseDto>(result);
    }
}
