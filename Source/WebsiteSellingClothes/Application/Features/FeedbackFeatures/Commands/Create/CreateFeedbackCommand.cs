using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Commands.Create;
public class CreateFeedbackCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public FeedbackRequestDto? FeedbackRequestDto { get; set; }
}
