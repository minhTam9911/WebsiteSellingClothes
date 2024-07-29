using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Queries.GetById;
public class GetByIdBrandQueryHandler : IRequestHandler<GetByIdBrandQuery, BrandResponseDto>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;

    public GetByIdBrandQueryHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
    }
     
    public async Task<BrandResponseDto> Handle(GetByIdBrandQuery request, CancellationToken cancellationToken)
    {
        var brand = await brandRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<BrandResponseDto>(brand);
        return data;
    }
}
