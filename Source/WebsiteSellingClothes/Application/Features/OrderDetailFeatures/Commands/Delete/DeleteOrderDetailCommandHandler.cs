using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderDetailFeatures.Commands.Delete;
public class DeleteOrderDetailCommandHandler : IRequestHandler<DeleteOrderDetailCommand, ServiceContainerResponseDto>
{
    private readonly IOrderDetailRepository orderDetailRepository;

    public DeleteOrderDetailCommandHandler(IOrderDetailRepository orderDetailRepository)
    {
        this.orderDetailRepository = orderDetailRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteOrderDetailCommand request, CancellationToken cancellationToken)
    {
        var result = await orderDetailRepository.DeleteAsync(request.Id, request.UserId);
        if (result > 0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
