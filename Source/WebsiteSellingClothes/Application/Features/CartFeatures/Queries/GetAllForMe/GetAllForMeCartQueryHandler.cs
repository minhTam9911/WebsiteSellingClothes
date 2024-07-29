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

namespace Application.Features.CartFeatures.Queries.GetAllForMe;
public class GetAllForMeCartQueryHandler : IRequestHandler<GetAllForMeCartQuery, PagedListDto<CartResponseDto>>
{
    private readonly ICartRepository cartRepository;
    private readonly IMapper mapper;

    public GetAllForMeCartQueryHandler(ICartRepository cartRepository, IMapper mapper)
    {
        this.cartRepository = cartRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<CartResponseDto>> Handle(GetAllForMeCartQuery request, CancellationToken cancellationToken)
    {
        var carts = await cartRepository.GetAllForMeAsync(request.FilterDto!, request.UserId);
        var data = new PagedListDto<CartResponseDto>() {
        
            PageIndex = carts!.PageIndex,
            PageSize = carts.PageSize,
            TotalCount = carts.TotalCount,
            Data = mapper.Map<List<CartResponseDto>>(carts.Data)
        };
        return data;
    }
}
