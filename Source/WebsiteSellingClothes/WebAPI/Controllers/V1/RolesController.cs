using Application.DTOs.Requests;
using Application.Features.RoleFeatures.Commands.Create;
using Application.Features.RoleFeatures.Queries.GetList;
using Application.Features.Roles.Queries.GetAll;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class RolesController : ApiControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] RoleRequestDto roleRequestDto)
    {
        var result = await Sender.Send(new CreateRoleCommand() { RoleRequestDto = roleRequestDto });
        if(result.Flag)return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleRequestDto roleRequestDto)
    {
        var result = await Sender.Send(new UpdateRoleCommand() {Id = id, RoleRequestDto = roleRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete( int id)
    {
        var result = await Sender.Send(new DeleteRoleCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllRoleQuery());
        if (data.Count > 0) return Ok(new {data = data});
        return BadRequest(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListRoleQuery() { FilterDto = filterDto});
        return Ok(data);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdRoleQuery() { Id = id});
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

}
