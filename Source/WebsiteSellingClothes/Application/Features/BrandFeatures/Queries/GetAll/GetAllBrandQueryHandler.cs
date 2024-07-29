using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Queries.GetAll;
public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQuery, List<BrandResponseDto>>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;

    public GetAllBrandQueryHandler(IMapper mapper, IBrandRepository brandRepository)
    {
        this.mapper = mapper;
        this.brandRepository = brandRepository;
    }

    public async Task<List<BrandResponseDto>> Handle(GetAllBrandQuery request, CancellationToken cancellationToken)
    {
        var brands = await brandRepository.GetAllAsync();
        var data = mapper.Map<List<BrandResponseDto>>(brands);
        return data;
    }
}
