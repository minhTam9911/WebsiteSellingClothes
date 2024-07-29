using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Commands.Update;
public class UpdateMerchantCommand : IRequest<DischargeWithDataResponseDto<MerchantResponseDto>>
{
    public string Id { get; set; } = string.Empty;
    public MerchantRequestDto? MerchantRequestDto { get; set; }
}
