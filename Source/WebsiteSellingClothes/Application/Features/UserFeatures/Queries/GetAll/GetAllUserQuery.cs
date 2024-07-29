using Application.DTOs.Responses;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetAll;
public class GetAllUserQuery : IRequest<List<UserResponseDto>>
{
}
