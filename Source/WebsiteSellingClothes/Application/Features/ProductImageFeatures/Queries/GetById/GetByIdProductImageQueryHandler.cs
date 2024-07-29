using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetById;
public class GetByIdProductImageQueryHandler : IRequestHandler<GetByIdProductImageQuery, ProductImageResponseDto>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IMapper mapper;

    public GetByIdProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.mapper = mapper;
    }

    public async Task<ProductImageResponseDto> Handle(GetByIdProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImage = await productImageRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<ProductImageResponseDto>(productImage);
        return data;
    }
}
