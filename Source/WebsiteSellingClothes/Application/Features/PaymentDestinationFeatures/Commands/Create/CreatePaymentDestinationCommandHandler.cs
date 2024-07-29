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

namespace Application.Features.PaymentDestinationFeatures.Commands.Create;
public class CreatePaymentDestinationCommandHandler : IRequestHandler<CreatePaymentDestinationCommand, DischargeWithDataResponseDto<PaymentDestinationResponseDto>>
{
    private readonly IMapper mapper;
    private readonly IPaymentDestinationRepository paymentDestinationRepository;

    public CreatePaymentDestinationCommandHandler(IMapper mapper, IPaymentDestinationRepository paymentDestinationRepository)
    {
        this.mapper = mapper;
        this.paymentDestinationRepository = paymentDestinationRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentDestinationResponseDto>> Handle(CreatePaymentDestinationCommand request, CancellationToken cancellationToken)
    {
        var paymentDestination = mapper.Map<PaymentDestination>(request.PaymentDestinationRequestDto);
        if (string.IsNullOrWhiteSpace(request.PaymentDestinationRequestDto!.PaymentDestinationParentId)) paymentDestination.PaymentDestinationParent = null;
        else paymentDestination.PaymentDestinationParent = await paymentDestinationRepository.GetByIdAsync(request.PaymentDestinationRequestDto.PaymentDestinationParentId);
        var result = await paymentDestinationRepository.InsertAsync(paymentDestination);
        if (result == null)
        {
            var data = new DischargeWithDataResponseDto<PaymentDestinationResponseDto>()
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
            var data = new DischargeWithDataResponseDto<PaymentDestinationResponseDto>()
            {

                Flag = true,
                Message = "Inserted",
                Status = (int)HttpStatusCode.OK,
                Data = mapper.Map<PaymentDestinationResponseDto>(result)

            };
            return data;
        }
    }
}
