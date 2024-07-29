using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;

namespace Application.Features.BrandFeatures.Queries.GetList;
public class GetListBrandQueryHandler : IRequestHandler<GetListBrandQuery, PagedListDto<BrandResponseDto>>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;

    public GetListBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<BrandResponseDto>> Handle(GetListBrandQuery request, CancellationToken cancellationToken)
    {
        var brands = await brandRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<BrandResponseDto>()
        {
            PageIndex = brands!.PageIndex,
            PageSize = brands.PageIndex,
            TotalCount = brands.TotalCount,
            Data = mapper.Map<List<BrandResponseDto>>(brands.Data)
        };
        return data;
    }
}
