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

namespace Application.Features.CategoryFeatures.Commands.Update;
public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ServiceContainerResponseDto>
{
    private readonly ICategoryRepository categoryRepository;
    private readonly IMapper mapper;

    public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
    {
        this.categoryRepository = categoryRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = mapper.Map<Category>(request.CategoryRequestDto);
        var result = await categoryRepository.UpdateAsync(request.Id, category, request.CategoryRequestDto!.Image);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");

    }
}
