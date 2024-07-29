using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FeedbackFeatures.Commands.Update;
public class UpdateFeedbackCommandHandler : IRequestHandler<UpdateFeedbackCommand, ServiceContainerResponseDto>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;
    private readonly IProductRepository productRepository;
    private readonly IUserRepository userRepository;

    public UpdateFeedbackCommandHandler(IFeedbackRepository feedbackRepository, IMapper mapper, IProductRepository productRepository, IUserRepository userRepository)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
        this.productRepository = productRepository;
        this.userRepository = userRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = mapper.Map<Feedback>(request.FeedbackRequestDto);
        feedback.Product = await productRepository.GetByIdAsync(request.FeedbackRequestDto!.ProductId);
        feedback.User = await userRepository.GetByIdAsync(request.UserId);
        var result = await feedbackRepository.UpdateAsync(request.Id,feedback);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");

    }
}
