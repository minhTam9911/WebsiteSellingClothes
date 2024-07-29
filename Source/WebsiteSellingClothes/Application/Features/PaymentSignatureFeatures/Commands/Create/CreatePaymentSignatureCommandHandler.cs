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

namespace Application.Features.PaymentSignatureFeatures.Commands.Create;
public class CreatePaymentSignatureCommandHandler : IRequestHandler<CreatePaymentSignatureCommand, DischargeWithDataResponseDto<PaymentSignatureResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentSignatureRepository paymentSignatureRepository;
    private readonly IPaymentRepository paymentRepository;
    public CreatePaymentSignatureCommandHandler(IMapper mapper, IPaymentSignatureRepository paymentSignatureRepository, IPaymentRepository paymentRepository)
    {
        this.mapper = mapper;
        this.paymentSignatureRepository = paymentSignatureRepository;
        this.paymentRepository = paymentRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentSignatureResponseDto>> Handle(CreatePaymentSignatureCommand request, CancellationToken cancellationToken)
    {
        var paymentSignature = mapper.Map<PaymentSignature>(request.PaymentSignatureRequestDto);
        paymentSignature.Payment = await paymentRepository.GetByIdAsync(request.PaymentSignatureRequestDto!.PaymentId);
        var result = await paymentSignatureRepository.InsertAsync(paymentSignature);
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
