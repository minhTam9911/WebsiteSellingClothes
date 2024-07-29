using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Queries.GetAll;
public class GetAllPaymentDestinationQueryHandler : IRequestHandler<GetAllPaymentDestinationQuery, List<PaymentDestinationResponseDto>>
{
    private readonly IPaymentDestinationRepository paymentDestinationRepository;
    private readonly IMapper mapper;

    public GetAllPaymentDestinationQueryHandler(IPaymentDestinationRepository paymentDestinationRepository, IMapper mapper)
    {
        this.paymentDestinationRepository = paymentDestinationRepository;
        this.mapper = mapper;
    }

    public async Task<List<PaymentDestinationResponseDto>> Handle(GetAllPaymentDestinationQuery request, CancellationToken cancellationToken)
    {
        var paymentDestinations = await paymentDestinationRepository.GetAllAsync();
        var data = mapper.Map<List<PaymentDestinationResponseDto>>(paymentDestinations);
        return data;
    }
}
