using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetAllForProduct;
public class GetAllForProductFeebackQuery : IRequest<PagedListDto<FeedbackResponseDto>>
{
    public string Code { get; set; } = string.Empty;
    public FilterDto? FilterDto { get; set; }
}
