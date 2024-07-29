using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentFeatures.Queries.GetList;
public class GetListPaymentQuery : IRequest<PagedListDto<PaymentResponseDto>>
{
    public FilterDto FilterDto { get; set; } = new FilterDto();
}
