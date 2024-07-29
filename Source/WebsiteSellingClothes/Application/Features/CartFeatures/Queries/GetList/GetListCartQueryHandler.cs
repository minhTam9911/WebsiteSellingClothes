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

namespace Application.Features.CartFeatures.Queries.GetList;
public class GetListCartQueryHandler : IRequestHandler<GetListCartQuery, PagedListDto<CartResponseDto>>
{
    private readonly ICartRepository cartRepository;
    private readonly IMapper mapper;

    public GetListCartQueryHandler(ICartRepository cartRepository, IMapper mapper)
    {
        this.cartRepository = cartRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<CartResponseDto>> Handle(GetListCartQuery request, CancellationToken cancellationToken)
    {
        var carts = await cartRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<CartResponseDto>()
        {
            PageIndex = carts!.PageIndex,
            PageSize = carts.PageSize,
            TotalCount = carts.TotalCount,
            Data = mapper.Map<List<CartResponseDto>>(carts.Data)
        };
        return data;
    }
}
