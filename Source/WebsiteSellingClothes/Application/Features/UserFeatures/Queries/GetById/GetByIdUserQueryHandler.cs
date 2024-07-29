using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetById;
public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponseDto>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetByIdUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<UserResponseDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByIdAsync(request.UserId);
        var data = mapper.Map<UserResponseDto>(user);
        return data;
    }
}
