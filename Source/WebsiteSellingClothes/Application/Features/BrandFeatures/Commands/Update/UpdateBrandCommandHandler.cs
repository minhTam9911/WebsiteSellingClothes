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

namespace Application.Features.BrandFeatures.Commands.Update;
public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommand, ServiceContainerResponseDto>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;
    public UpdateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = mapper.Map<Brand>(request.BrandRequestDto);
        var result = await brandRepository.UpdateAsync(request.Id, brand, request.BrandRequestDto!.Image);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");

    }
}
