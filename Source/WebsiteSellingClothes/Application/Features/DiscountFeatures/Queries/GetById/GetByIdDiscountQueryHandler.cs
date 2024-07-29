using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Queries.GetById;
public class GetByIdDiscountQueryHandler : IRequestHandler<GetByIdDiscountQuery, DiscountResponseDto>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public GetByIdDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<DiscountResponseDto> Handle(GetByIdDiscountQuery request, CancellationToken cancellationToken)
    {
        var discount = await discountRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<DiscountResponseDto>(discount);
        return data;
    }
}
