using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System.Net;

namespace Application.Features.CategoryFeatures.Commands.Create;
public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ServiceContainerResponseDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;
    public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request.CategoryRequestDto);
        var result = await categoryRepository.InsertAsync(category,request.CategoryRequestDto!.Image!);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
