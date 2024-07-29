﻿using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetByRole;
public class GetByRoleUserQuery : IRequest<List<UserResponseDto>>
{
    public string Name { get; set; } = string.Empty;
}
