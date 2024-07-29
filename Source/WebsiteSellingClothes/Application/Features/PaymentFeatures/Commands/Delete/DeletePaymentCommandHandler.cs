using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.Delete;
public class DeletePaymentCommandHandler : IRequestHandler<DeletePaymentCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentRepository paymentRepository;

    public DeletePaymentCommandHandler(IPaymentRepository paymentRepository)
    {
        this.paymentRepository = paymentRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentRepository.DeleteAsync(request.Id);
        if (result > 0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
