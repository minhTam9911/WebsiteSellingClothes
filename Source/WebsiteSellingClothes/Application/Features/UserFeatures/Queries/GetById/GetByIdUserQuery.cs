using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Queries.GetById;
public class GetByIdUserQuery : IRequest<UserResponseDto>
{
    public Guid UserId { get; set; }
}
