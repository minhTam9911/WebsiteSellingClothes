using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Commands.Update;
public class UpdatePaymentSignatureCommandHandler : IRequestHandler<UpdatePaymentSignatureCommand, DischargeWithDataResponseDto<PaymentSignatureResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentSignatureRepository paymentSignatureRepository;

    public UpdatePaymentSignatureCommandHandler(IMapper mapper, IPaymentSignatureRepository paymentSignatureRepository)
    {
        this.mapper = mapper;
        this.paymentSignatureRepository = paymentSignatureRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentSignatureResponseDto>> Handle(UpdatePaymentSignatureCommand request, CancellationToken cancellationToken)
    {
        var paymentSignature = mapper.Map<PaymentSignature>(request.PaymentSignatureRequestDto);
        var result = await paymentSignatureRepository.UpdateAsync(request.Id, paymentSignature);
        if (result == null)
        {
            var data = new DischargeWithDataResponseDto<PaymentSignatureResponseDto>()
            {

                Flag = false,
                Message = "Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Data = null,

            };
            return data;
        }
        else
        {
            var data = new DischargeWithDataResponseDto<PaymentSignatureResponseDto>()
            {

                Flag = true,
                Message = "Inserted",
                Status = (int)HttpStatusCode.OK,
                Data = mapper.Map<PaymentSignatureResponseDto>(result)

            };
            return data;
        }
    }
}
