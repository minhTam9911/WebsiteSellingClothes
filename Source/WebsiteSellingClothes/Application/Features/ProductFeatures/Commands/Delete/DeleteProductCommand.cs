﻿using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Commands.Delete;
public class DeleteProductCommand : IRequest<ServiceContainerResponseDto>
{
    public int Id { get; set; }
}
