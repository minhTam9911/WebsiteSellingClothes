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

namespace Application.Features.FeedbackFeatures.Queries.GetAllForProduct;
public class GetAllForProductFeebackQueryHandler : IRequestHandler<GetAllForProductFeebackQuery, PagedListDto<FeedbackResponseDto>>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;

    public GetAllForProductFeebackQueryHandler(IFeedbackRepository feedbackRepository, IMapper mapper)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
    }

    public async Task<PagedListDto<FeedbackResponseDto>> Handle(GetAllForProductFeebackQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await feedbackRepository.GetAllForProductAsync(request.Code, request.FilterDto!);
        var data = new PagedListDto<FeedbackResponseDto>() {
        
            PageIndex = feedbacks!.PageIndex,
            PageSize = feedbacks.PageSize,
            TotalCount = feedbacks.TotalCount,
            Data = mapper.Map<List<FeedbackResponseDto>>(feedbacks.Data)
        };
        return data;
    }
}
