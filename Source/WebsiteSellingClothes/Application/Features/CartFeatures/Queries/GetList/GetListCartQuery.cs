﻿using Application.DTOs.Responses;
using Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CartFeatures.Queries.GetList;
public class GetListCartQuery : IRequest<PagedListDto<CartResponseDto>>
{
    public FilterDto? FilterDto { get; set; }
}
