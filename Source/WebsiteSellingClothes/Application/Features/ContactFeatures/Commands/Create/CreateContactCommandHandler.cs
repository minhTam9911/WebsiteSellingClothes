using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Commands.Create;
public class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, ServiceContainerResponseDto>
{
    private readonly IContactRepository contactRepository;
    private readonly IMapper mapper;

    public CreateContactCommandHandler(IContactRepository contactRepository, IMapper mapper)
    {
        this.contactRepository = contactRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = mapper.Map<Contact>(request.ContactRequestDto);
        var result = await contactRepository.InsertAsync(contact);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
