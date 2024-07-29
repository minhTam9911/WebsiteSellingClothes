using Application.DTOs.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.PaymentNotificationFeatures.Queries.GetAll;
public class GetAllPaymentNotificationQuery : IRequest<List<PaymentNotificationResponseDto>>
{
}
