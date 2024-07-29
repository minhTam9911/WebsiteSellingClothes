using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetAllActive;
public class GetAllActiveProductQueryHandler : IRequestHandler<GetAllActiveProductQuery, PagedListDto<ProductResponseDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetAllActiveProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<ProductResponseDto>> Handle(GetAllActiveProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetAllActiveAsync(request.IsActive, request.FilterDto!);
        var data = new PagedListDto<ProductResponseDto>()
        {
            PageIndex = products!.PageIndex,
            PageSize = products.PageSize,
            TotalCount = products.TotalCount,
            Data = mapper.Map<List<ProductResponseDto>>(products.Data)
        };
        return data;
    }
}
