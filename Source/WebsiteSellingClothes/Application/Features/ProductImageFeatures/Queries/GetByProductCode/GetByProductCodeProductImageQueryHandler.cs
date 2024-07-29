using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetByProductCode;
public class GetByProductCodeProductImageQueryHandler : IRequestHandler<GetByProductCodeProductImageQuery, List<ProductImageResponseDto>>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IMapper mapper;

    public GetByProductCodeProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductImageResponseDto>> Handle(GetByProductCodeProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImages = await productImageRepository.GetByProductCodeAsync(request.ProductCode);
        var data = mapper.Map<List<ProductImageResponseDto>>(productImages);
        return data;
    }
}
