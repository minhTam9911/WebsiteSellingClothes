using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetById;
public class GetByIdFeedbackQueryHandler : IRequestHandler<GetByIdFeedbackQuery, FeedbackResponseDto>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;

    public GetByIdFeedbackQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
    }

    public async Task<FeedbackResponseDto> Handle(GetByIdFeedbackQuery request, CancellationToken cancellationToken)
    {
        var feedback = await feedbackRepository.GetByIdAsync(request.Id);
        var data = mapper.Map<FeedbackResponseDto>(feedback);
        return data;
    }
}
