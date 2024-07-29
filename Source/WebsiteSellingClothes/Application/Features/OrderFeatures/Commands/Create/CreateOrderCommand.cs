using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.OrderFeatures.Commands.Create;
public class CreateOrderCommand : IRequest<Order>
{
    public Guid UserId { get; set; }
    public OrderRequestDto? OrderRequestDto {  get; set; }
}
