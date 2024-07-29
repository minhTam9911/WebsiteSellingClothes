using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Commands.Delete;
public class DeletePaymentTransactionCommandHandler : IRequestHandler<DeletePaymentTransactionCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;

    public DeletePaymentTransactionCommandHandler(IPaymentTransactionRepository paymentTransactionRepository)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeletePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentTransactionRepository.DeleteAsync(request.Id);
        if (result > 0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true,"Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
