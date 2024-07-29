using Application.DTOs.Responses;
using Application.DTOs.VnPays;
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

namespace Application.Features.PaymentFeatures.Commands.Update;
public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, DischargeWithDataResponseDto<PaymentLinkDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public UpdatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<PaymentLinkDto>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = mapper.Map<Payment>(request.PaymentRequestDto);
        var result = await paymentRepository.UpdateAsync(request.Id, payment);
        if (result == null)
        {
            var data = new DischargeWithDataResponseDto<PaymentLinkDto>()
            {

                Flag = false,
                Message = "Error",
                Status = (int)HttpStatusCode.InternalServerError,
                Data = new PaymentLinkDto()

            };
            return data;
        }
        else
        {
            var data = new DischargeWithDataResponseDto<PaymentLinkDto>()
            {

                Flag = true,
                Message = "Success",
                Status = (int)HttpStatusCode.OK,
                Data = new PaymentLinkDto()

            };
            return data;
        }
    }
}
