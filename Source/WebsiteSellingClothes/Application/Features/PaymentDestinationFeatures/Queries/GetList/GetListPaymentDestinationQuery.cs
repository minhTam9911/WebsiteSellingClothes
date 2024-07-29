using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentDestinationFeatures.Queries.GetList;
public class GetListPaymentDestinationQuery : IRequest<PagedListDto<PaymentDestinationResponseDto>>
{
    public FilterDto FilterDto { get; set; } = new FilterDto();
}
