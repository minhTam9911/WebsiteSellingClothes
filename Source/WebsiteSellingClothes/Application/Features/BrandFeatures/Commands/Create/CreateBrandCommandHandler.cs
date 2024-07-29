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

namespace Application.Features.BrandFeatures.Commands.Create;
public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, ServiceContainerResponseDto>
{
    private readonly IBrandRepository brandRepository;
    private readonly IMapper mapper;
    public CreateBrandCommandHandler(IBrandRepository brandRepository, IMapper mapper)
    {
        this.brandRepository = brandRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
    {
        var brand = mapper.Map<Brand>(request.BrandRequestDto);
        var result = await brandRepository.InsertAsync(brand,request.BrandRequestDto!.Image!);
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
