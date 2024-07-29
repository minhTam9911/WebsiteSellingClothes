using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Queries.GetById;
public class GetByIdPaymentQueryHandler : IRequestHandler<GetByIdPaymentQuery,DischargeWithDataResponseDto<PaymentResponseDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetByIdPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<PaymentResponseDto>> Handle(GetByIdPaymentQuery request, CancellationToken cancellationToken)
    {
        var payment = await paymentRepository.GetByIdAsync(request.Id);
        var data = new DischargeWithDataResponseDto<PaymentResponseDto>();
        if(payment == null)
        {
            data.Status = (int)HttpStatusCode.NotFound;
            data.Flag = false;
            data.Message = "Error";
            data.Data = null;
            return data;
        }
        var paymentResponse = mapper.Map<PaymentResponseDto>(payment);
        data.Status = (int)HttpStatusCode.OK;
        data.Flag = true;
        data.Message = "Success";
        data.Data = paymentResponse;
        return data;
    }
}
