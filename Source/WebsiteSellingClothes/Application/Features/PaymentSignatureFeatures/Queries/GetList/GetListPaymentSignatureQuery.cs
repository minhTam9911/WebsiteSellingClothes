using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Queries.GetList;
public class GetListPaymentSignatureQuery : IRequest<PagedListDto<PaymentSignatureResponseDto>>
{
    public FilterDto FilterDto { get; set; } = new FilterDto();
}
