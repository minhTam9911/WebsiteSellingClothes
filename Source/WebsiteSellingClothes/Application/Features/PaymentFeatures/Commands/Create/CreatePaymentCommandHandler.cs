using Application.DTOs.Momos;
using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using Application.DTOs.ZaloPays;
using AutoMapper;
using Common.Extensions;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.Create;
public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, DischargeWithDataResponseDto<PaymentLinkDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IPaymentDestinationRepository paymentDestinationRepository;
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IAuthRepository authRepository;
    private readonly IOrderRepository orderRepository;

    public CreatePaymentCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IAuthRepository authRepository, IPaymentDestinationRepository paymentDestinationRepository, IMerchantRepository merchantRepository, IOrderRepository orderRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.authRepository = authRepository;
        this.paymentDestinationRepository = paymentDestinationRepository;
        this.merchantRepository = merchantRepository;
        this.orderRepository = orderRepository;
    }

    public async Task<DischargeWithDataResponseDto<PaymentLinkDto>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var payment = mapper.Map<Payment>(request.PaymentRequestDto);
        payment.PaymentDestination = await paymentDestinationRepository.GetByIdAsync(request.PaymentRequestDto!.PaymentDestinationId) ?? throw new BadHttpRequestException("Payment destination not found");
        payment.Merchant = await merchantRepository.GetByIdAsync(request.PaymentRequestDto.MerchantId) ?? throw new BadHttpRequestException("Merchant not found");
        payment.PaymentStatus = "1";
       
        var result = await paymentRepository.InsertAsync(payment,request.Order!, request.UserId);
        var paymentUrl = string.Empty;
        var message = string.Empty;
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
            switch (request.PaymentRequestDto!.PaymentDestinationId)
            {
                case "DEST001":
                    var vnPay = new VnPayRequest(
                        version: configuration["VnPayOptions:Version"]!,
                        tmnCode: configuration["VnPayOptions:TmnCode"]!,
                        createDate: DateTime.Now,
                        ipAddress: authRepository.IpAddress(),
                        amount: payment.RequiredAmount,
                        currCode: payment.PaymentCurrency,
                        orderType: "Orther",
                        orderInfo: payment.PaymentContent,
                        returnUrl: configuration["VnPayOptions:ReturnUrl"]!,
                        txnRef: result!.Id,
                        locale: payment.PaymentLanguage
                        
                        );
                    paymentUrl = vnPay.GetLink(configuration["VnPayOptions:PaymentUrl"]!, configuration["VnPayOptions:HashSecret"]!);
                    break;
                case "DEST002":
                    var momo = new MomoOneTimePaymentRequest(
                        partnerCode : configuration["MomoOptions:PartnerCode"]!,
                        redirectUrl: configuration["MomoOptions:ReturnUrl"]!,
                        ipnUrl: configuration["MomoOptions:IpnUrl"]!,
                        amount : (long)request.PaymentRequestDto.RequiredAmount,
                        orderId : request.PaymentRequestDto.OrderId.ToString(),
                        lang: "vi",
                        requestId : result.Id,
                        requestType : "captureWallet",
                        extraData :string.Empty,
                        orderInfo : $"Thanh Toan Hoa Don {payment.OrderId}"
                    );
                    momo.MakeSignature(configuration["MomoOptions:AccessKey"]!, configuration["MomoOptions:SecretKey"]!);
                    (bool createMomoResult, string? createMessage) 
                        = await momo.GetLink(configuration["MomoOptions:PaymentUrl"]!);
                    if (createMomoResult)
                    {
                        paymentUrl = createMessage;
                    }
                    else
                    {
                        message = createMessage;
                    }
                    break;
                case "DEST003":
                    var zalo = new ZaloPayRequest(
                            appId: int.Parse(configuration["ZaloPayOptions:AppId"]!),
                            appUser: configuration["ZaloPayOptions:AppUser"]!,
                            appTime: DateTime.Now.GetTimeStamp(),
                            amount: Convert.ToInt64(payment.RequiredAmount),
                            appTransId:payment.Id!,
                            bankCode: "zalopayapp",
                            description: $"Thank Toan Hoa Don {payment.OrderId}",
                            callBackUrl: configuration["ZaloPayOptions:CallBackUrl"]!,
                            redirectUrl : configuration["ZaloPayOptions:ReturnUrl"]!
                        );
                    zalo.MakeSignature(configuration["ZaloPayOptions:Key1"]!);
                    (bool createZaloPayResult, string? createMessageZaloPay)
                        = await zalo.GetLink(configuration["ZaloPayOptions:PaymentUrl"]!);
                    if (createZaloPayResult)
                    {
                        paymentUrl = createMessageZaloPay;
                    }
                    else
                    {
                        message = createMessageZaloPay;
                    }
                    break;
                    
            }
            var data = new DischargeWithDataResponseDto<PaymentLinkDto>()
            {

                Flag = string.IsNullOrWhiteSpace(message)? true:false,
                Message = string.IsNullOrWhiteSpace(message) ? "Inserted":message,
                Status = string.IsNullOrWhiteSpace(message) ? (int)HttpStatusCode.OK: (int)HttpStatusCode.BadRequest,
                Data = new PaymentLinkDto() { PaymentId = result.Id, PaymentUrl = paymentUrl!}

            };
            return data;
        }
    }
}
