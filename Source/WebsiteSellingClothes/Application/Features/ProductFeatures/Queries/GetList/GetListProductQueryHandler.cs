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

namespace Application.Features.ProductFeatures.Queries.GetList;
public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, PagedListDto<ProductResponseDto>>
{
    private readonly IProductRepository productRepository;
    private readonly IMapper mapper;

    public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
    {
        this.productRepository = productRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<ProductResponseDto>> Handle(GetListProductQuery request, CancellationToken cancellationToken)
    {
        var products = await productRepository.GetListAsync(request.FilterDto!);
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
