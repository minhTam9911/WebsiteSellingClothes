using Application.DTOs.Requests;
using Application.Features.UserFeatures.Commands.Create;
using Application.Features.UserFeatures.Commands.Delete;
using Application.Features.UserFeatures.Commands.Update;
using Application.Features.UserFeatures.Commands.UploadImage;
using Application.Features.UserFeatures.Queries.GetAll;
using Application.Features.UserFeatures.Queries.GetById;
using Application.Features.UserFeatures.Queries.GetByRole;
using Application.Features.UserFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class UsersController : ApiControllerBase
{

    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] UserRequestDto userRequestDto)
    {
        var result = await Sender.Send(new CreateUserCommand() { UserRequestDto = userRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UserRequestDto userRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new UpdateUserCommand() { UserId = userId, UserRequestDto = userRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new DeleteUserCommand() { UserId = userId});
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize]
    [HttpPut("UploadImage")]
    public async Task<IActionResult> UploadImage([FromForm] UserImageRequestDto userImageRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new UploadImageUserCommand()
        {
            UserId = userId,
            UserImageRequestDto = userImageRequestDto
        });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllUserQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize]
    [HttpGet("my-user")]
    public async Task<IActionResult> GetById()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdUserQuery() { UserId = userId });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var data = await Meditor.Send(new GetByIdUserQuery() { UserId = id });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("role/{name}")]
    public async Task<IActionResult> GetByRole(string name)
    {
        var data = await Meditor.Send(new GetByRoleUserQuery() { Name = name});
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListUserQuery() { FilterDto = filterDto });
        return Ok(data);
    }
}
