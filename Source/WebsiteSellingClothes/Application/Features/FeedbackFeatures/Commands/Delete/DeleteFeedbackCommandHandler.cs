using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.FeedbackFeatures.Commands.Delete;
public class DeleteFeedbackCommandHandler : IRequestHandler<DeleteFeedbackCommand, ServiceContainerResponseDto>
{
    private readonly IFeedbackRepository feedbackRepository;

    public DeleteFeedbackCommandHandler(IFeedbackRepository feedbackRepository)
    {
        this.feedbackRepository = feedbackRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteFeedbackCommand request, CancellationToken cancellationToken)
    {
        var result = await feedbackRepository.DeleteAsync(request.Id, request.UserId);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
