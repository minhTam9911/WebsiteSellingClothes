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

namespace Application.Features.UserFeatures.Queries.GetList;
public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, PagedListDto<UserResponseDto>>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public GetListUserQueryHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<UserResponseDto>> Handle(GetListUserQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<UserResponseDto>()
        {
            PageIndex = users!.PageIndex,
            PageSize = users.PageSize,
            TotalCount = users.TotalCount,
            Data = mapper.Map<List<UserResponseDto>>(users.Data)
        };
        return data;
    }
}
