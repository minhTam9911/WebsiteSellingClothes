using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Commands.Delete;
public class DeleteMerchantCommandHandler : IRequestHandler<DeleteMerchantCommand, ServiceContainerResponseDto>
{
    private readonly IMerchantRepository merchantRepository;

    public DeleteMerchantCommandHandler(IMerchantRepository merchantRepository)
    {
        this.merchantRepository = merchantRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteMerchantCommand request, CancellationToken cancellationToken)
    {
        var result = await merchantRepository.DeleteAsync(request.Id);
        if (result > 0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
