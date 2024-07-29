using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentSignatureFeatures.Queries.GetAllForMe;
public class GetAllForMePaymentSignatureQuery : IRequest<PagedListDto<PaymentSignatureResponseDto>>
{
    public Guid UserId { get; set; }
    public FilterDto FilterDto { get; set; } = new FilterDto();
}
