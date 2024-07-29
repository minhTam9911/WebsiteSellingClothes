using Application.DTOs.Responses;
using Application.Features.CategoryFeatures.Queries.GetList;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetList;
public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, PagedListDto<CategoryResponseDto>>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<CategoryResponseDto>> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<CategoryResponseDto>()
        {
            PageIndex = categories!.PageIndex,
            PageSize = categories.PageSize,
            TotalCount = categories.TotalCount,
            Data = mapper.Map<List<CategoryResponseDto>>(categories.Data)
        };
        return data;
    }
}
