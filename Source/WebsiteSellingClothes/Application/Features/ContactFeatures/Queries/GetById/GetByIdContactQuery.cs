using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Queries.GetById;
public class GetByIdContactQuery : IRequest<ContactResponseDto>
{
    public int Id { get; set; }
}
