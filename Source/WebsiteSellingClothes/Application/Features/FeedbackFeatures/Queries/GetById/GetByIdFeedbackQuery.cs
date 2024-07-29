using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetById;
public class GetByIdFeedbackQuery : IRequest<FeedbackResponseDto>
{
    public int Id { get; set; }
}
