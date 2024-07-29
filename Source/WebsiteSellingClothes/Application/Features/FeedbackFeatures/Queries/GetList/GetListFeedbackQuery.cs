using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetList;
public class GetListFeedbackQuery : IRequest<PagedListDto<FeedbackResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
