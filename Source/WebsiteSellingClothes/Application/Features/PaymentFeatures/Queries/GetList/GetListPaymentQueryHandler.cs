using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Queries.GetList;
public class GetListPaymentQueryHandler : IRequestHandler<GetListPaymentQuery, PagedListDto<PaymentResponseDto>>
{
    private readonly IPaymentRepository paymentRepository;
    private readonly IMapper mapper;

    public GetListPaymentQueryHandler(IPaymentRepository paymentRepository, IMapper mapper)
    {
        this.paymentRepository = paymentRepository;
        this.mapper = mapper;
    }

    public Task<PagedListDto<PaymentResponseDto>> Handle(GetListPaymentQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
