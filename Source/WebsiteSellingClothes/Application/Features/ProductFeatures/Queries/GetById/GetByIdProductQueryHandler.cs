using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetById;
public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductResponseDto>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<ProductResponseDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<ProductResponseDto>(product);
        return data;
    }
}
