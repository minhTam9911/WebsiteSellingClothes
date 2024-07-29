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

namespace Application.Features.DiscountFeatures.Queries.GetList;
public class GetListDiscountQueryHandler : IRequestHandler<GetListDiscountQuery, PagedListDto<DiscountResponseDto>>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public GetListDiscountQueryHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<DiscountResponseDto>> Handle(GetListDiscountQuery request, CancellationToken cancellationToken)
    {
        var discounts = await discountRepository.GetListAsync(request.FilterDto!);
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
