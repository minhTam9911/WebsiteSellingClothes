using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Queries.GetById;
public class GetByIdMerchantQueryHandler : IRequestHandler<GetByIdMerchantQuery, MerchantResponseDto>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public GetByIdMerchantQueryHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<MerchantResponseDto> Handle(GetByIdMerchantQuery request, CancellationToken cancellationToken)
    {
        var merchant = await merchantRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<MerchantResponseDto>(merchant);
        return data;
    }
}
