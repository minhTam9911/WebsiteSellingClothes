using Application.DTOs.Responses;
using Application.DTOs.VnPays;
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

namespace Application.Features.MerchantFeatures.Commands.Create;
public class CreateMerchantCommandHandler : IRequestHandler<CreateMerchantCommand, DischargeWithDataResponseDto<MerchantResponseDto>>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public CreateMerchantCommandHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<MerchantResponseDto>> Handle(CreateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = mapper.Map<Merchant>(request.MerchantRequestDto);
        var result = await merchantRepository.InsertAsync(merchant, request.UserId);
        var data = new DischargeWithDataResponseDto<MerchantResponseDto>();
        if (result == null)
        {
            data.Flag = false;
            data.Message = "Error";
            data.Status = (int)HttpStatusCode.InternalServerError;
            data.Data = new MerchantResponseDto();
            return data;
        }
        data.Flag = true;
        data.Message = "Inserted";
        data.Status = (int)HttpStatusCode.OK;
        data.Data = mapper.Map<MerchantResponseDto>(result);
        return data;
    }
}
