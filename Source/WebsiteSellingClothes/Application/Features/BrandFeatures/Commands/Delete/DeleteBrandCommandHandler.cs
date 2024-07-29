using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Commands.Delete;
public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommand, ServiceContainerResponseDto>
{
    private readonly IBrandRepository brandRepository;

    public DeleteBrandCommandHandler(IBrandRepository brandRepository)
    {
        this.brandRepository = brandRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
    {
        var result = await brandRepository.DeleteAsync(request.Id);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
