using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.DiscountFeatures.Commands.Delete;
public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand, ServiceContainerResponseDto>
{
    private readonly IDiscountRepository discountRepository;

    public DeleteDiscountCommandHandler(IDiscountRepository discountRepository)
    {
        this.discountRepository = discountRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
    {
        var result = await discountRepository.DeleteAsync(request.Id);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
