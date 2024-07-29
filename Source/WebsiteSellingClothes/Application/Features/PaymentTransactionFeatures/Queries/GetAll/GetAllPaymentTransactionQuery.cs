using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentTransactionFeatures.Queries.GetAll;
public class GetAllPaymentTransactionQuery : IRequest<List<PaymentTransactionResponseDto>>
{
}
