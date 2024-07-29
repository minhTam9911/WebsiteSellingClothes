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

namespace Application.Features.CategoryFeatures.Queries.GetAllActive;
public class GetAllActiveCategoryQueryHandler : IRequestHandler<GetAllActiveCategoryQuery, PagedListDto<CategoryResponseDto>>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetAllActiveCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<CategoryResponseDto>> Handle(GetAllActiveCategoryQuery request, CancellationToken cancellationToken)
    {
       var categories = await categoryRepository.GetAllActiveAsync(request.IsActive,request.PageSize,request.PageIndex);
        var data = new PagedListDto<CategoryResponseDto>()
        {
            PageIndex = categories!.PageIndex,
            PageSize = categories.PageSize,
            TotalCount = categories.TotalCount,
            Data = mapper.Map<IReadOnlyList<CategoryResponseDto>>(categories.Data)
        };
        return data;
    }
}
