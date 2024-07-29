using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Delete;
public class DeletePaymentSignatureCommandHandler : IRequestHandler<DeletePaymentSignatureCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentSignatureRepository paymentSignatureRepository;

    public DeletePaymentSignatureCommandHandler(IPaymentSignatureRepository paymentSignatureRepository)
    {
        this.paymentSignatureRepository = paymentSignatureRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeletePaymentSignatureCommand request, CancellationToken cancellationToken)
    {
        var result = await paymentSignatureRepository.DeleteAsync(request.Id);
        if(result>0) return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
        return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
    }
}
