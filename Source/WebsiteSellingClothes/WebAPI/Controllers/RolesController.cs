using Application.DTOs.Requests;
using Application.Features.RoleFeatures.Commands.Create;
using Application.Features.Roles.Queries.GetAll;
using Domain.DTOs.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RolesController : ApiController
{
	[HttpGet]
	public async Task<IActionResult> GetList([FromQuery]FilterRequestDto filterRequestDto)
	{
		//var data = await Meditor.Send(new GetAllRoleQuery() { FilterRequestDto = filterRequestDto });
		//return Ok(data);

		int max = 999999999;
		string[] parts = "PC{0:D9}/POS".Split('/');
		// is it safe to assume that the third splitted string is the number you're looking for?
		int number;
		if (Int32.TryParse(parts[2], out number))
		{
			number++;
		}
		else
		{
		number = 0;
		}
		number = (number > max) ? 1 : number;
		return Ok(number);
	}

	[HttpPost]
	public async Task<IActionResult> Insert([FromBody] RoleRequestDto roleRequestDto)
	{
		var data = await Meditor.Send(new CreateRoleCommand() { RoleRequestDto = roleRequestDto });
		return Ok(data);
	}

}
