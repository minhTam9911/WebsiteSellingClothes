﻿using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.BrandFeatures.Commands.Create;
public class CreateBrandCommand : IRequest<ServiceContainerResponseDto>
{

    public BrandRequestDto? BrandRequestDto { get; set; }

}
