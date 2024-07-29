using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Queries.GetAll;
public class GetAllCartQueryHandler : IRequestHandler<GetAllCartQuery, List<CartResponseDto>>
{
    private readonly ICartRepository cartRepository;
    private readonly IMapper mapper;

    public GetAllCartQueryHandler(ICartRepository cartRepository, IMapper mapper)
    {
        this.cartRepository = cartRepository;
        this.mapper = mapper;
    }

    public async Task<List<CartResponseDto>> Handle(GetAllCartQuery request, CancellationToken cancellationToken)
    {
        var carts = await cartRepository.GetAllAsync();
        var data = mapper.Map<List<CartResponseDto>>(carts);
        return data;
    }
}
