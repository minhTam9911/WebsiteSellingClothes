using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetByProductId;
public class GetByProductIdProductImageQueryHandler : IRequestHandler<GetByProductIdProductImageQuery, List<ProductImageResponseDto>>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IMapper mapper;

    public GetByProductIdProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductImageResponseDto>> Handle(GetByProductIdProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImage = await productImageRepository.GetByProductIdAsync(request.ProductId);
        var data = mapper.Map<List<ProductImageResponseDto>>(productImage);
        return data;
    }
}
