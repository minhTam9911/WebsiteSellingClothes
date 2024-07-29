using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetByRole;
public class GetByRoleUserQueryHandler : IRequestHandler<GetByRoleUserQuery, List<UserResponseDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetByRoleUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<List<UserResponseDto>> Handle(GetByRoleUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetByRoleAsync(request.Name);
        var data = mapper.Map<List<UserResponseDto>>(users);
        return data;
    }
}
