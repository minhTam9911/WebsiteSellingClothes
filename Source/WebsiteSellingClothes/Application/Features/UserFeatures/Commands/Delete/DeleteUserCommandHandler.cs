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

namespace Application.Features.UserFeatures.Commands.Delete;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ServiceContainerResponseDto>
{
    private readonly IUserRepository userRepository;
    private readonly IMapper mapper;

    public DeleteUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
    }

    public async Task<ServiceContainerResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var result = await userRepository.DeleteAsync(request.UserId);
        if(result <=0) return new ServiceContainerResponseDto((int)HttpStatusCode.BadRequest, false, "Failure");
        return new ServiceContainerResponseDto((int)HttpStatusCode.OK,true,"Deleted");
    }
}
