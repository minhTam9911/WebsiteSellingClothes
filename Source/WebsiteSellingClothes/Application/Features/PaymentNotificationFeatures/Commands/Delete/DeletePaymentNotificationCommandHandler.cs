using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Commands.Delete;
public class DeletePaymentNotificationCommandHandler : IRequestHandler<DeletePaymentNotificationCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentNotificationRepository paymentNotificationRepository;

    public DeletePaymentNotificationCommandHandler(IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.paymentNotificationRepository = paymentNotificationRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeletePaymentNotificationCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentNotificationRepository.DeleteAsync(request.Id);
        if (result > 0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
