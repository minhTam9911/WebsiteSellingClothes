using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Queries.GetById;
public class GetByIdContactQueryHandler : IRequestHandler<GetByIdContactQuery, ContactResponseDto>
{
    private readonly IContactRepository contactRepository;
    private readonly IMapper mapper;

    public GetByIdContactQueryHandler(IContactRepository contactRepository, IMapper mapper)
    {
        this.contactRepository = contactRepository;
        this.mapper = mapper;
    }

    public async Task<ContactResponseDto> Handle(GetByIdContactQuery request, CancellationToken cancellationToken)
    {
        var contact = await contactRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<ContactResponseDto>(contact);
        return data;
    }
}
