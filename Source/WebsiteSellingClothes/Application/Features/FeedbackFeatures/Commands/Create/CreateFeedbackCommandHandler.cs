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

namespace Application.Features.FeedbackFeatures.Commands.Create;
public class CreateFeedbackCommandHandler : IRequestHandler<CreateFeedbackCommand, ServiceContainerResponseDto>
{
    private readonly IFeedbackRepository feedbackRepository;
    private readonly IMapper mapper;
    private readonly IUserRepository userRepository;
    private readonly IProductRepository productRepository;

    public CreateFeedbackCommandHandler(IFeedbackRepository feedbackRepository, IMapper mapper, IUserRepository userRepository, IProductRepository productRepository)
    {
        this.feedbackRepository = feedbackRepository;
        this.mapper = mapper;
        this.userRepository = userRepository;
        this.productRepository = productRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
    {
        var feedback = mapper.Map<Feedback>(request.FeedbackRequestDto);
        feedback.Product = await productRepository.GetByIdAsync(request.FeedbackRequestDto!.ProductId);
        feedback.User = await userRepository.GetByIdAsync(request.UserId);
        var result = await feedbackRepository.InsertAsync(feedback);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
