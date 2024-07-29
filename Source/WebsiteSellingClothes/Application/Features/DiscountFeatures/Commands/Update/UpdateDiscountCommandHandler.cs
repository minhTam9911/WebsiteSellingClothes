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

namespace Application.Features.DiscountFeatures.Commands.Update;
public class UpdateDiscountCommandHandler : IRequestHandler<UpdateDiscountCommand, ServiceContainerResponseDto>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public UpdateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = mapper.Map<Discount>(request.DiscountRequestDto);
        var result = await discountRepository.UpdateAsync(request.Id, discount, request.DiscountRequestDto!.ProductsId.ToArray());
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");
    }
}
