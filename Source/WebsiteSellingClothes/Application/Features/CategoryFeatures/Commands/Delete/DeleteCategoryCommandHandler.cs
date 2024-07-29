using Application.DTOs.Responses;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CategoryFeatures.Commands.Delete;
public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ServiceContainerResponseDto>
{
    private readonly ICategoryRepository categoryRepository;

    public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
    {
        this.categoryRepository = categoryRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await categoryRepository.DeleteAsync(request.Id);
        if (result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Deleted");
    }
}
