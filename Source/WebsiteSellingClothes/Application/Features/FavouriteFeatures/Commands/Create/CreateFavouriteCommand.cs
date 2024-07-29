using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.FavouriteFeatures.Commands.Create;
public class CreateFavouriteCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public FavouriteRequestDto? FavouriteRequestDto { get; set; }
}
