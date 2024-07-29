using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Queries.GetAll;
public class GetAllDiscountQueryHandler : IRequestHandler<GetAllDiscountQuery, List<DiscountResponseDto>>
{
    public readonly IDiscountRepository discountRepository;
    public readonly IMapper mapper;

    public GetAllDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<List<DiscountResponseDto>> Handle(GetAllDiscountQuery request, CancellationToken cancellationToken)
    {
        var discounts = await discountRepository.GetAllAsync();
        var data = mapper.Map<List<DiscountResponseDto>>(discounts);
        return data;
    }
}
