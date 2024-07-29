using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Commands.Create;
public class CreateMerchantCommand : IRequest<DischargeWithDataResponseDto<MerchantResponseDto>>
{
    public Guid UserId { get; set; }
    public MerchantRequestDto? MerchantRequestDto { get; set; }
}
