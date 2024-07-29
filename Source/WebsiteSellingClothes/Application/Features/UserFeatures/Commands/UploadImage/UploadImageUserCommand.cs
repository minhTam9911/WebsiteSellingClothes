using Application.DTOs.Requests;
using Application.DTOs.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UploadImage;
public class UploadImageUserCommand : IRequest<ServiceContainerResponseDto>
{
    public Guid UserId { get; set; }
    public UserImageRequestDto? UserImageRequestDto { get; set; }

}
