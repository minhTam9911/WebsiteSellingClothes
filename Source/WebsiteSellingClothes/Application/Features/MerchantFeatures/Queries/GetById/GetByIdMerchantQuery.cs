using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.MerchantFeatures.Queries.GetById;
public class GetByIdMerchantQuery : IRequest<MerchantResponseDto>
{
    public string Id { get; set; } = string.Empty;
}
