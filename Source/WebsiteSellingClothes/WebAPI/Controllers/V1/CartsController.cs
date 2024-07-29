using Application.DTOs.Requests;
using Application.Features.CartFeatures.Commands.Create;
using Application.Features.CartFeatures.Commands.Delete;
using Application.Features.CartFeatures.Commands.Update;
using Application.Features.CartFeatures.Queries.GetAll;
using Application.Features.CartFeatures.Queries.GetAllForMe;
using Application.Features.CartFeatures.Queries.GetById;
using Application.Features.CartFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CartsController : ApiControllerBase
{
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] CartRequestDto cartRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new CreateCartCommand() {UserId = userId, CartRequestDto = cartRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromBody] CartRequestDto cartRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new UpdateCartCommand() { UserId = userId, CartRequestDto = cartRequestDto,Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new DeleteCartCommand() { UserId = userId, Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllCartQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize]
    [HttpGet("my-cart")]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var data = await Meditor.Send(new GetAllForMeCartQuery() { UserId = userId, FilterDto = filterDto});
        return Ok(data);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListCartQuery() {FilterDto = filterDto });
        return Ok(data);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdCartQuery() {Id = id,UserId =userId});
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }
}
