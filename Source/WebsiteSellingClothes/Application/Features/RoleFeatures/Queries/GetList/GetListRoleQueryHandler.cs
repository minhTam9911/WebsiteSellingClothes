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

namespace Application.Features.RoleFeatures.Queries.GetList;
public class GetListRoleQueryHandler : IRequestHandler<GetListRoleQuery, PagedListDto<RoleResponseDto>>
{
    private readonly IRoleRepository roleRepository;
    private readonly IMapper mapper;

    public GetListRoleQueryHandler(IRoleRepository roleRepository, IMapper mapper)
    {
        this.roleRepository = roleRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<RoleResponseDto>> Handle(GetListRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await roleRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<RoleResponseDto>()
        {
            PageIndex = roles!.PageIndex,
            PageSize = roles.PageSize,
            TotalCount = roles.TotalCount,
            Data = mapper.Map<List<RoleResponseDto>>(roles.Data)
        };
        return data;
    }
}
