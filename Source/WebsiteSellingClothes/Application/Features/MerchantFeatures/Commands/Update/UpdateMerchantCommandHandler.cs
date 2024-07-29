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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.MerchantFeatures.Commands.Update;
public class UpdateMerchantCommandHandler : IRequestHandler<UpdateMerchantCommand, DischargeWithDataResponseDto<MerchantResponseDto>>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public UpdateMerchantCommandHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<MerchantResponseDto>> Handle(UpdateMerchantCommand request, CancellationToken cancellationToken)
    {
        var merchant = mapper.Map<Merchant>(request.MerchantRequestDto);
        var result = await merchantRepository.UpdateAsync(request.Id, merchant);
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
        data.Message = "Updated";
        data.Status = (int)HttpStatusCode.OK;
        data.Data = mapper.Map<MerchantResponseDto>(result);
        return data;
    }
}
