using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Queries.GetAll;
public class GetAllContactQueryHandler : IRequestHandler<GetAllContactQuery, List<ContactResponseDto>>
{
    private readonly IContactRepository contactRepository;
    private readonly IMapper mapper;

    public GetAllContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        this.contactRepository = contactRepository;
        this.mapper = mapper;
    }

    public async Task<List<ContactResponseDto>> Handle(GetAllContactQuery request, CancellationToken cancellationToken)
    {
        var contacts = await contactRepository.GetAllAsync();
        var data = mapper.Map<List<ContactResponseDto>>(contacts);
        return data;
    }
}
