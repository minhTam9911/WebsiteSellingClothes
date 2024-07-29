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

namespace Application.Features.BrandFeatures.Queries.GetAllActive;
public class GetAllActiveBrandQueryHandler : IRequestHandler<GetAllActiveBrandQuery, PagedListDto<BrandResponseDto>>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;

    public GetAllActiveBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<BrandResponseDto>> Handle(GetAllActiveBrandQuery request, CancellationToken cancellationToken)
    {
       var brands = await brandRepository.GetAllActiveAsync(request.IsActive,request.PageSize,request.PageIndex);
        var data = new PagedListDto<BrandResponseDto>()
        {
            PageIndex = brands!.PageIndex,
            PageSize = brands.PageSize,
            TotalCount = brands.TotalCount,
            Data = mapper.Map<List<BrandResponseDto>>(brands.Data)
        };
        return data;
    }
}
