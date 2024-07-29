using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Queries.GetById;
public class GetByIdPaymentDestinationQueryHandler : IRequestHandler<GetByIdPaymentDestinationQuery, PaymentDestinationResponseDto>
{
    private readonly IMapper mapper;
    private readonly IPaymentDestinationRepository paymentDestinationRepository;

    public GetByIdPaymentDestinationQueryHandler(IMapper mapper, IPaymentDestinationRepository paymentDestinationRepository)
    {
        this.mapper = mapper;
        this.paymentDestinationRepository = paymentDestinationRepository;
    }

    public async Task<PaymentDestinationResponseDto> Handle(GetByIdPaymentDestinationQuery request, CancellationToken cancellationToken)
    {
        var paymentDestination = await paymentDestinationRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<PaymentDestinationResponseDto>(paymentDestination);
        return data;
    }
}
