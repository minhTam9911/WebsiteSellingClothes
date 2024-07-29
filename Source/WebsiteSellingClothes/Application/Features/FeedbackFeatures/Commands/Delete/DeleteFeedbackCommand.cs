using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Commands.Delete;
public class DeleteFeedbackCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
}
