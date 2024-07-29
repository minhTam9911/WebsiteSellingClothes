using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Commands.Delete;
public class DeleteFavouriteCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public int Id { get; set; }
}
