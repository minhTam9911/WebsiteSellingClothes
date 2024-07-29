using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Queries.GetById;
public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryResponseDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<CategoryResponseDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
    {
        var brand = await categoryRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<CategoryResponseDto>(brand);
        return data;
    }
}
