using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetAll;
public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, List<ProductResponseDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductResponseDto>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllAsync();
        var data = mapper.Map<List<ProductResponseDto>>(products);
        return data;
    }
}
