using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Commands.Delete;
public class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand, ServiceContainerResponseDto>
{
    private readonly IContactRepository contactRepository;

    public DeleteContactCommandHandler(IContactRepository contactRepository)
    {
        this.contactRepository = contactRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteContactCommand request, CancellationToken cancellationToken)
    {
        var result = await contactRepository.DeleteAsync(request.Id);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
