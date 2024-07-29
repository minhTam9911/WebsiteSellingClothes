using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Queries.GetAll;
public class GetAllMerchantQueryHandler : IRequestHandler<GetAllMerchantQuery, List<MerchantResponseDto>>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public GetAllMerchantQueryHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<List<MerchantResponseDto>> Handle(GetAllMerchantQuery request, CancellationToken cancellationToken)
    {
        var merchants = await merchantRepository.GetAllAsync();
        var data = mapper.Map<List<MerchantResponseDto>>(merchants);
        return data;
    }
}
