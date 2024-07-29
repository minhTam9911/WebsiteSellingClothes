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

namespace Application.Features.PaymentTransactionFeatures.Commands.Update;
public class UpdatePaymentTransactionCommandHandler : IRequestHandler<UpdatePaymentTransactionCommand, DischargeWithDataResponseDto<PaymentTransactionResponseDto>>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    public readonly IMapper mapper;

    public UpdatePaymentTransactionCommandHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<PaymentTransactionResponseDto>> Handle(UpdatePaymentTransactionCommand request, CancellationToken cancellationToken)
    {
        var paymentTransaction = mapper.Map<PaymentTransaction>(request.PaymentTransactionRequestDto);
        var result = await paymentTransactionRepository.UpdateAsync(request.Id, paymentTransaction);
        if (result == null)
        {
            var data = new DischargeWithDataResponseDto<PaymentTransactionResponseDto>()
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
            var data = new DischargeWithDataResponseDto<PaymentTransactionResponseDto>()
            {

                Flag = true,
                Message = "Inserted",
                Status = (int)HttpStatusCode.OK,
                Data = mapper.Map<PaymentTransactionResponseDto>(result)

            };
            return data;
        }
    }
}
