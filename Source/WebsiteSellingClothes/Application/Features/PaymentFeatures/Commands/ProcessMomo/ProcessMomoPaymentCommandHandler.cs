using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.PaymentFeatures.Commands.ProcessMomo;
public class ProcessMomoPaymentCommandHandler : IRequestHandler<ProcessMomoPaymentCommand, DischargeWithDataResponseDto<(PaymentReturnDto, string)>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;

    public ProcessMomoPaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
    }

    public async Task<DischargeWithDataResponseDto<(PaymentReturnDto, string)>> Handle(ProcessMomoPaymentCommand request, CancellationToken cancellationToken)
    {
        var returnUrl = string.Empty;
        var momoPayment = new PaymentReturnDto();
        var isValidSignature = request.IsValidSignature(configuration["MomoOptions:AccessKey"]!, configuration["MomoOptions:SecretKey"]!);
        var data = new DischargeWithDataResponseDto<(PaymentReturnDto, string)>();
        if (isValidSignature)
        {
            if (request.resultCode == "0")
            {
                var payment = await paymentRepository.GetByIdAsync(request.requestId);
                if (payment == null)
                {
                    momoPayment.PaymentStatus = "10";
                    momoPayment.PaymentMessage = "Can't find Payment at payment service";
                }
                else
                {
                    var merchant = await merchantRepository.GetByIdAsync(payment.Merchant!.Id);
                    returnUrl = merchant!.MerchantReturnUrl ?? string.Empty;
                    momoPayment.Signature = Guid.NewGuid().ToString();
                    momoPayment.PaymentStatus = "00";
                    momoPayment.PaymentId = payment.Id;


                }
            }
            else
            {
                momoPayment.PaymentStatus = "10";
                momoPayment.PaymentMessage = "Payment process failed";
            }
            data.Status = (int)HttpStatusCode.OK;
            data.Message = "Success";
            data.Flag = true;
            data.Data = (momoPayment, returnUrl);

            return data;
        }
        else
        {
            momoPayment.PaymentStatus = "99";
            momoPayment.PaymentMessage = "Invalid signature in response";
        }
        data.Status = (int)HttpStatusCode.InternalServerError;
        data.Message = "Error";
        data.Flag = false;
        data.Data = (null, string.Empty)!;
        return data;

    }
}

