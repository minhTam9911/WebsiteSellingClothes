using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Commands.Delete;
public class DeletePaymentDestinationCommandHandler : IRequestHandler<DeletePaymentDestinationCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentDestinationRepository paymentDestinationRepository;

    public DeletePaymentDestinationCommandHandler(IPaymentDestinationRepository paymentDestinationRepository)
    {
        this.paymentDestinationRepository = paymentDestinationRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeletePaymentDestinationCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentDestinationRepository.DeleteAsync(request.Id);
        if(result>0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
