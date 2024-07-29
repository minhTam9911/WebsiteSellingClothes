using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Queries.GetAll;
public class GetAllPaymentQueryHandler : IRequestHandler<GetAllPaymentQuery, List<PaymentResponseDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetAllPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public async Task<List<PaymentResponseDto>> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
    {
        var payments = await paymentRepository.GetAllAsync();
        var data = mapper.Map<List<PaymentResponseDto>>(payments);
        return data;
    }
}
