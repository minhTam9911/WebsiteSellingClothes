using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductImageFeatures.Queries.GetAll;
public class GetAllProductImageQueryHandler : IRequestHandler<GetAllProductImageQuery, List<ProductImageResponseDto>>
{
    private readonly IProductImageRepository productImageRepository;
    private readonly IMapper mapper;

    public GetAllProductImageQueryHandler(IProductImageRepository productImageRepository, IMapper mapper)
    {
        this.productImageRepository = productImageRepository;
        this.mapper = mapper;
    }

    public async Task<List<ProductImageResponseDto>> Handle(GetAllProductImageQuery request, CancellationToken cancellationToken)
    {
        var productImages = await productImageRepository.GetAllAsync();
        var data = mapper.Map<List<ProductImageResponseDto>>(productImages);
        return data;
    }

}
