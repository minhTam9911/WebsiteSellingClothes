using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using AutoMapper;
using Common.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Commands.ProcessVnPayIpn;
public class ProcessVnPayIpnCommandHandler : IRequestHandler<ProcessVnPayIpnCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;

    public ProcessVnPayIpnCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository, IPaymentTransactionRepository paymentTransactionRepository, IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.paymentNotificationRepository = paymentNotificationRepository;
    }
    public async Task<ServiceContainerResponseDto> Handle(ProcessVnPayIpnCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var isValidSignature = request.IsValidSignature(configuration["VnPayOptions:HashSecret"]!);
            if (isValidSignature)
            {
                var payment = await paymentRepository.GetByIdAsync(request.vnp_TxnRef);
                if (payment == null) throw new BadHttpRequestException("Payment does not exist");
                else
                {
                    if (payment.RequiredAmount == decimal.Parse((request.vnp_Amount / 100).ToString()!))
                    {
                        if (payment.PaymentStatus == "0")
                        {
                            return new ServiceContainerResponseDto((int)HttpStatusCode.NotFound, false, "Order already comfirm");

                        }
                        else
                        {
                            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
                            {
                                var paymentSaveChange = await paymentRepository.SetPaidAsync(request.vnp_TxnRef, decimal.Parse(request.vnp_Amount.ToString()!)/100);
                                if (paymentSaveChange > 0)
                                {
                                    var paymentTransaction = new PaymentTransaction()
                                    {
                                        Id = string.Empty,
                                        Payment = payment,
                                        TransactionAmount = decimal.Parse(request.vnp_Amount.ToString()!),
                                        TransactionDate = DateTime.ParseExact(request.vnp_PayDate, "yyyyMMddHHmmss", CultureInfo.InvariantCulture),
                                        TransactionMessage = "Transaction Successfully!",
                                        TransactionStatus = "00",
                                        TransactionPayload = JsonConvert.SerializeObject(mapper.Map<List<OrderDetailResponseDto>>(payment.Order!.OrderDetails))
                                    };
                                    await paymentTransactionRepository.InsertAsync(paymentTransaction);
                                    var paymentNotification = new PaymentNotification()
                                    {
                                        Id = string.Empty,
                                        Merchant = payment.Merchant,
                                        NotificaitonDate = DateTime.Now,
                                        NotificationAmount = request.vnp_Amount.ToString() + " VND",
                                        NotificationContent = "Order Successfully",
                                        NotificationMessage = $"You have successfully paid for order #{payment.OrderId}",
                                        NotificationSignature = GenerateHelper.GenerateSecretKey(),
                                        NotificationStatus = "00",
                                        NotificationResDate = DateTime.Now,
                                        Payment = payment
                                    };
                                    await paymentNotificationRepository.InsertAsync(paymentNotification);
                                    return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Confirm success");

                                }
                                else
                                {
                                    return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Input required data");
                                }
                            }
                        }
                    }
                    else
                    {
                        return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Invalid Amount");

                    }
                    return new ServiceContainerResponseDto((int)HttpStatusCode.NotFound, false, "Order not found");
                }
            }
            else
            {
                return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Invalid Signture");

            }
        }
        catch (Exception ex)
        {
            return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, ex.Message);

        }
    }
}
