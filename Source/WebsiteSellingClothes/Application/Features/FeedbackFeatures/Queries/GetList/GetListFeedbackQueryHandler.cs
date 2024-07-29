using Application.DTOs.Responses;
using AutoMapper;
using Common.DTOs;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Queries.GetList;
public class GetListFeedbackQueryHandler : IRequestHandler<GetListFeedbackQuery, PagedListDto<FeedbackResponseDto>>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;

    public GetListFeedbackQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<FeedbackResponseDto>> Handle(GetListFeedbackQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await feedbackRepository.GetListAsync(request.FilterDto!);
        var data = new PagedListDto<FeedbackResponseDto>()
        {
            PageIndex = feedbacks!.PageIndex,
            PageSize = feedbacks.PageSize,
            TotalCount = feedbacks.TotalCount,
            Data = mapper.Map<List<FeedbackResponseDto>>(feedbacks.Data)
        };
        return data;
    }
}
