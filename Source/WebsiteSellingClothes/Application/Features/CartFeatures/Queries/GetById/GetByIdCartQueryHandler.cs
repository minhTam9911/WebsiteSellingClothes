using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Queries.GetById;
public class GetByIdCartQueryHandler : IRequestHandler<GetByIdCartQuery, CartResponseDto>
{
    private readonly ICartRepository cartRepository;
    private readonly IMapper mapper;

    public GetByIdCartQueryHandler(ICartRepository cartRepository, IMapper mapper)
    {
        this.cartRepository = cartRepository;
        this.mapper = mapper;
    }

    public async Task<CartResponseDto> Handle(GetByIdCartQuery request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.Id,request.UserId);
        var data = mapper.Map<CartResponseDto>(cart);
        return data;
    }
}
