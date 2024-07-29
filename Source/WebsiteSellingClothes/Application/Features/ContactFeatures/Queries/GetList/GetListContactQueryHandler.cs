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

namespace Application.Features.ContactFeatures.Queries.GetList;
public class GetListContactQueryHandler : IRequestHandler<GetListContactQuery, PagedListDto<ContactResponseDto>>
{
    private readonly IContactRepository contactRepository;
    private readonly IMapper mapper;

    public GetListContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        this.contactRepository = contactRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<ContactResponseDto>> Handle(GetListContactQuery request, CancellationToken cancellationToken)
    {
        var contacts = await contactRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<ContactResponseDto>()
        {
            PageIndex = contacts!.PageIndex,
            PageSize = contacts.PageSize,
            TotalCount = contacts.TotalCount,
            Data = mapper.Map<List<ContactResponseDto>>(contacts.Data)
        };
        return data;
    }
}
