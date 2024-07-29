using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetAll;
public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<UserResponseDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetAllUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<List<UserResponseDto>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync();
        var data = mapper.Map<List<UserResponseDto>>(users);
        return data;
    }
}
