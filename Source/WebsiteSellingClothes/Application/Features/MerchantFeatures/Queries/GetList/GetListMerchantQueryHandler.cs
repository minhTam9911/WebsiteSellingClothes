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

namespace Application.Features.MerchantFeatures.Queries.GetList;
public class GetListMerchantQueryHandler : IRequestHandler<GetListMerchantQuery, PagedListDto<MerchantResponseDto>>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public GetListMerchantQueryHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<MerchantResponseDto>> Handle(GetListMerchantQuery request, CancellationToken cancellationToken)
    {
        var merchants = await merchantRepository.GetListAsync(request.FilterDto);
        var data = new PagedListDto<MerchantResponseDto>() {
        
            PageIndex = merchants.PageIndex,
            PageSize = merchants.PageSize,
            TotalCount = merchants.TotalCount,
            Data = mapper.Map<List<MerchantResponseDto>>(merchants.Data)

        };
        return data;
    }
}
