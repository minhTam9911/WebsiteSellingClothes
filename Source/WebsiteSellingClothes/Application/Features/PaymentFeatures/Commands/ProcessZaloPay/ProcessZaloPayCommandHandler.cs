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

namespace Application.Features.PaymentFeatures.Commands.ProcessZaloPay;
public class ProcessZaloPayCommandHandler : IRequestHandler<ProcessZaloPayCommand, DischargeWithDataResponseDto<(PaymentReturnDto, string)>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;

    public ProcessZaloPayCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
    }
    public async Task<DischargeWithDataResponseDto<(PaymentReturnDto, string)>> Handle(ProcessZaloPayCommand request, CancellationToken cancellationToken)
    {
        var data = new DischargeWithDataResponseDto<(PaymentReturnDto, string)>();
        var zalopay = new PaymentReturnDto();
        var returnUrl = string.Empty;
        var isValidSignature = request.IsValidSignature(configuration["ZaloPayOptions:Key2"]!);
        if (isValidSignature)
        {
            if (request.status == 1)
            {
                var payment = await paymentRepository.GetByIdAsync(request.apptransid);
                if (payment == null)
                {
                    zalopay.PaymentStatus = "10";
                    zalopay.PaymentMessage = "Can't find Payment at payment service";
                }
                else
                {
                    var merchant = await merchantRepository.GetByIdAsync(payment.Merchant!.Id);
                    returnUrl = merchant!.MerchantReturnUrl ?? string.Empty;
                    zalopay.Signature = Guid.NewGuid().ToString();
                    zalopay.PaymentStatus = "00";
                    zalopay.PaymentId = payment.Id;


                }
            }
            else
            {
                zalopay.PaymentStatus = "10";
                zalopay.PaymentMessage = "Payment process failed";
            }
            data.Status = (int)HttpStatusCode.OK;
            data.Message = "Success";
            data.Flag = true;
            data.Data = (zalopay, returnUrl);

            return data;
        }
        else
        {
            zalopay.PaymentStatus = "99";
            zalopay.PaymentMessage = "Invalid signature in response";
        }
        data.Status = (int)HttpStatusCode.InternalServerError;
        data.Message = "Error";
        data.Flag = false;
        data.Data = (null, string.Empty)!;
        return data;

    }

}
