using Application.DTOs.Responses;
using AutoMapper;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Features.MerchantFeatures.Commands.SetActive;
public class SetActiveMerchantCommandHandler : IRequestHandler<SetActiveMerchantCommand, DischargeWithDataResponseDto<MerchantResponseDto>>
{
    private readonly IMerchantRepository merchantRepository;
    private readonly IMapper mapper;

    public SetActiveMerchantCommandHandler(IMerchantRepository merchantRepository, IMapper mapper)
    {
        this.merchantRepository = merchantRepository;
        this.mapper = mapper;
    }

    public async Task<DischargeWithDataResponseDto<MerchantResponseDto>> Handle(SetActiveMerchantCommand request, CancellationToken cancellationToken)
    {
        var result = await merchantRepository.SetActiveAsync(request.Id);
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
