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

namespace Application.Features.DiscountFeatures.Queries.GetAllActive;
public class GetAllActiveDiscountQueryHandler : IRequestHandler<GetAllActiveDiscountQuery, PagedListDto<DiscountResponseDto>>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public GetAllActiveDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<DiscountResponseDto>> Handle(GetAllActiveDiscountQuery request, CancellationToken cancellationToken)
    {
        var discounts = await discountRepository.GetAllActiveAsync(request.IsActive, request.PageSize, request.PageSize);
        var data = new PagedListDto<DiscountResponseDto>()
        {
            PageIndex = discounts!.PageIndex,
            PageSize = discounts.PageSize,
            TotalCount = discounts.TotalCount,
            Data = mapper.Map<List<DiscountResponseDto>>(discounts.Data)
        };
        return data;
    }
}
