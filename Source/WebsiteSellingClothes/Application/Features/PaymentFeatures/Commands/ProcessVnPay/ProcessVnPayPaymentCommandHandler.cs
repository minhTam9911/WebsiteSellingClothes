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

namespace Application.Features.PaymentFeatures.Commands.ProcessVnPay;
public class ProcessVnPayPaymentCommandHandler : IRequestHandler<ProcessVnPayPaymentCommand, DischargeWithDataResponseDto<(PaymentReturnDto,string)>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;

    public ProcessVnPayPaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
    }

    public async Task<DischargeWithDataResponseDto<(PaymentReturnDto,string)>> Handle(ProcessVnPayPaymentCommand request, CancellationToken cancellationToken)
    {
       
        var data = new DischargeWithDataResponseDto<(PaymentReturnDto,string)>();
        var vnPayPayment = new PaymentReturnDto();
        var returnUrl = string.Empty;
        var isValidSignature =  request.IsValidSignature(configuration["VnPayOptions:HashSecret"]!);
        if(isValidSignature)
        {
            if(request.vnp_ResponseCode == "00")
            {
                var payment = await paymentRepository.GetByIdAsync(request.vnp_TxnRef);
                if(payment == null)
                {
                    vnPayPayment.PaymentStatus = "10";
                    vnPayPayment.PaymentMessage = "Can't find Payment at payment service";
                }
                else
                {
                    var merchant = await merchantRepository.GetByIdAsync(payment.Merchant!.Id);
                    returnUrl = merchant!.MerchantReturnUrl ?? string.Empty;
                    vnPayPayment.Signature = Guid.NewGuid().ToString();
                    vnPayPayment.PaymentStatus = "00";
                    vnPayPayment.PaymentId = payment.Id;

                    
                }
            }
            else
            {
                vnPayPayment.PaymentStatus = "10";
                vnPayPayment.PaymentMessage = "Payment process failed";
            }
            data.Status = (int)HttpStatusCode.OK;
            data.Message = "Success";
            data.Flag = true;
            data.Data = (vnPayPayment,returnUrl);
            
            return data;
        }
        else
        {
            vnPayPayment.PaymentStatus = "99";
            vnPayPayment.PaymentMessage = "Invalid signature in response";
        }
        data.Status = (int)HttpStatusCode.InternalServerError;
        data.Message = "Error";
        data.Flag = false;
        data.Data = (null, string.Empty)!;
        return data;

    }
}
    