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

namespace Application.Features.ProductImageFeatures.Queries.GetList;
public class GetListProductImageQueryHandler : IRequestHandler<GetListProductImageQuery, PagedListDto<ProductImageResponseDto>>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IMapper mapper;

    public GetListProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<ProductImageResponseDto>> Handle(GetListProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImages = await productImageRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<ProductImageResponseDto>()
        {
            PageIndex = productImages!.PageIndex,
            PageSize = productImages.PageSize,
            TotalCount = productImages.TotalCount,
            Data = mapper.Map<List<ProductImageResponseDto>>(productImages.Data)
        };
        return data;
    }
}
