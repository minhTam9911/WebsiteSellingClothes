﻿using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ProductFeatures.Queries.GetList;
public class GetListProductQuery : IRequest<PagedListDto<ProductResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
