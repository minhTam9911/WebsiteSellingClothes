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

namespace Application.Features.PaymentTransactionFeatures.Commands.Create;
public class CreatePaymentTransactionCommandHandler : IRequestHandler<CreatePaymenTransactionCommand, DischargeWithDataResponseDto<PaymentTransactionResponseDto>>
{
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IMapper mapper;
    private readonly IPaymentRepository paymentRepository;

    public CreatePaymentTransactionCommandHandler(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper, IPaymentRepository paymentRepository)
    {
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.mapper = mapper;
        this.paymentRepository = paymentRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentTransactionResponseDto>> Handle(CreatePaymenTransactionCommand request, CancellationToken cancellationToken)
    {
        var paymentTransaction = mapper.Map<PaymentTransaction>(request.PaymentTransactionRequestDto);
        paymentTransaction.Payment = await paymentRepository.GetByIdAsync(request.PaymentTransactionRequestDto!.PaymentId);
        var result = await paymentTransactionRepository.InsertAsync(paymentTransaction);
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
