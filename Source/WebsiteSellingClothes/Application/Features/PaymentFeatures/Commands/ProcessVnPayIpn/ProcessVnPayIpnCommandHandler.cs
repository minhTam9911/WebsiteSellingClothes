using Application.DTOs.Responses;
using Application.DTOs.VnPays;
using AutoMapper;
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

namespace Application.Features.PaymentFeatures.Commands.ProcessVnPayIpn;
public class ProcessVnPayIpnCommandHandler : IRequestHandler<ProcessVnPayIpnCommand, ServiceContainerResponseDto>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;
    private readonly IConfiguration configuration;
    private readonly IMerchantRepository merchantRepository;

    public ProcessVnPayIpnCommandHandler(IPaymentRepository paymentRepository, IMapper mapper, IConfiguration configuration, IMerchantRepository merchantRepository)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
        this.configuration = configuration;
        this.merchantRepository = merchantRepository;
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
                    if (payment.RequiredAmount == (request.vnp_Amount / 100))
                    {
                        if (payment.PaymentStatus == "0")
                        {
                            return new ServiceContainerResponseDto((int)HttpStatusCode.NotFound, false, "Order already comfirm");

                        }
                        else
                        {
                            if (request.vnp_ResponseCode == "00" && request.vnp_TransactionStatus == "00")
                            {
                                var paymentSaveChange = await paymentRepository.SetPaidAsync(request.vnp_TxnRef, decimal.Parse(request.vnp_Amount.ToString()!));
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
