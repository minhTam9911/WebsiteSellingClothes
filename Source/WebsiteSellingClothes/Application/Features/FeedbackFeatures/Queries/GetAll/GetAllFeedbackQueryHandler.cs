using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetAll;
public class GetAllFeedbackQueryHandler : IRequestHandler<GetAllFeedbackQuery, List<FeedbackResponseDto>>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;

    public GetAllFeedbackQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
    }

    public async Task<List<FeedbackResponseDto>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await feedbackRepository.GetAllAsync();
        var data = mapper.Map<List<FeedbackResponseDto>>(feedbacks);
        return data;
    }
}
