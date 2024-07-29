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

namespace Application.Features.UserFeatures.Commands.Create;
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ServiceContainerResponseDto>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = mapper.Map<User>(request.UserRequestDto);
        var result = await userRepository.InsertAsync(user);
        if(result == null) return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK,true,"Inserted");
    }
}
