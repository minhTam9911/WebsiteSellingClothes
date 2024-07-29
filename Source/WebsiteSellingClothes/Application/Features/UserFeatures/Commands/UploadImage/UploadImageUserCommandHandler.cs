using Application.DTOs.Responses;
using Common.Helpers;
using Domain.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.UploadImage;
public class UploadImageUserCommandHandler : IRequestHandler<UploadImageUserCommand, ServiceContainerResponseDto>
{

    private readonly IUserRepository userRepository;

    public UploadImageUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<ServiceContainerResponseDto> Handle(UploadImageUserCommand request, CancellationToken cancellationToken)
    {
        if (!FileHelper.IsImage(request.UserImageRequestDto!.Image!)) throw new IOException("The file uploaded does not have to be an image file ");
        var result = await userRepository.UploadImageAsync(request.UserId, request.UserImageRequestDto!.Image!);
        if (result <= 0) return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Uploaded");
    }
}
