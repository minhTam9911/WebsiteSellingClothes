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

namespace Application.Features.DiscountFeatures.Commands.Create;
public class CreateDiscountCommandHandler : IRequestHandler<CreateDiscountCommand, ServiceContainerResponseDto>
{
    private readonly IDiscountRepository discountRepository;
    private readonly IMapper mapper;

    public CreateDiscountCommandHandler(IDiscountRepository discountRepository, IMapper mapper)
    {
        this.discountRepository = discountRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = mapper.Map<Discount>(request.DiscountRequestDto);
        var result = await discountRepository.InsertAsync(discount, request.DiscountRequestDto!.ProductsId.ToArray());
        if (result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.InternalServerError, false, "Error");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Inserted");
    }
}
