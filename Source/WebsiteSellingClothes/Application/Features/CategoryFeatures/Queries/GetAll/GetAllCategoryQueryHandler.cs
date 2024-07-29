using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetAll;
public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, List<CategoryResponseDto>>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetAllCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<List<CategoryResponseDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync();
        var data = mapper.Map<List<CategoryResponseDto>>(categories);
        return data;
    }
}
