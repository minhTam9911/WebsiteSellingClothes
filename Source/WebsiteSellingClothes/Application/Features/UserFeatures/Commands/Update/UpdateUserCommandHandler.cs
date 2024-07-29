using Application.DTOs.Responses;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeatures.Commands.Update;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ServiceContainerResponseDto>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public UpdateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.UserRequestDto);
        var result = await userRepository.UpdateAsync(request.UserId,user);
        if(result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK, true, "Updated");
    }
}
