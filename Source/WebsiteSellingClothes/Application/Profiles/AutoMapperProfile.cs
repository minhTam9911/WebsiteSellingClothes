using Application.DTOs.Requests;
using Application.DTOs.Responses;
using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Profiles;
public class AutoMapperProfile: Profile
{
	
	public AutoMapperProfile()
	{
		CreateMap<RoleRequestDto, Role>();
		CreateMap<Role, RoleResponseDto>();
	
	}
}
