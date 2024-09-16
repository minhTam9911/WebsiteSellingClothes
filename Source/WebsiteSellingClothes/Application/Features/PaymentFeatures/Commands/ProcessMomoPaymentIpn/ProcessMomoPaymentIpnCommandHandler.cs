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

namespace Application.Features.PaymentFeatures.Commands.ProcessMomoPaymentIpn;
public class ProcessMomoPaymentIpnCommandHandler : IRequestHandler<ProcessMomoPaymentIpnCommand,ServiceContainerResponseDto>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;
    private readonly IPaymentTransactionRepository paymentTransactionRepository;
    private readonly IPaymentNotificationRepository paymentNotificationRepository;
    public ProcessMomoPaymentIpnCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository, IPaymentTransactionRepository paymentTransactionRepository, IPaymentNotificationRepository paymentNotificationRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
        this.paymentTransactionRepository = paymentTransactionRepository;
        this.paymentNotificationRepository = paymentNotificationRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(ProcessMomoPaymentIpnCommand request, CancellationToken cancellationToken)
    {
        var result = new object();
        try
        {
            var isValidSignature = request.IsValidSignature(configuration["MomoOptions:AccessKey"]!, configuration["MomoOptions:SecretKey"]!);
            if (isValidSignature)
            {
                var payment = await paymentRepository.GetByIdAsync(request.requestId);
                if (payment == null) throw new BadHttpRequestException("Payment does not exist");
                else
                {
                    if (payment.RequiredAmount == request.amount)
                    {
                        if (payment.PaymentStatus == "0")
                        {
                            var paymentTransaction = new PaymentTransaction()
                            {
                                Id = string.Empty,
                                Payment = payment,
                                TransactionAmount = decimal.Parse(request.amount.ToString()!),
                                TransactionDate = DateTimeHelper.ConvertTimeStampToDateTime(request.responseTime),
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
                                NotificationAmount = request.amount.ToString() + " VND",
                                NotificationContent = "Order Successfully",
                                NotificationMessage = $"You have successfully paid for order #{payment.OrderId}",
                                NotificationSignature = GenerateHelper.GenerateSecretKey(),
                                NotificationStatus = "00",
                                NotificationResDate = DateTime.Now,
                                Payment = payment
                            };
                            await paymentNotificationRepository.InsertAsync(paymentNotification);
                            return new ServiceContainerResponseDto((int)HttpStatusCode.NotFound, false, "Order already comfirm");
                        }
                        else
                        {
                            if (request.resultCode == "0")
                            {
                                var paymentSaveChange = await paymentRepository.SetPaidAsync(request.requestId, decimal.Parse(request.amount.ToString()!));
                                if (paymentSaveChange > 0)
                                {
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
                    return new ServiceContainerResponseDto((int)HttpStatusCode.NotFound, false, "Order not found comfirm");
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
