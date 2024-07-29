using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ContactFeatures.Queries.GetAll;
public class GetAllContactQuery : IRequest<List<ContactResponseDto>>
{
}
