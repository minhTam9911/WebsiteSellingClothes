using Application.DTOs.Requests;
using Application.Features.FavouriteFeatures.Commands.Create;
using Application.Features.FavouriteFeatures.Commands.Delete;
using Application.Features.FavouriteFeatures.Queries.GetAll;
using Application.Features.FavouriteFeatures.Queries.GetAllForMe;
using Application.Features.FavouriteFeatures.Queries.GetById;
using Application.Features.FavouriteFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FavouritesController : ApiControllerBase
{
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FavouriteRequestDto favouriteRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new CreateFavouriteCommand() { FavouriteRequestDto = favouriteRequestDto,UserId = userId });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new DeleteFavouriteCommand() { UserId = userId, Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllFavouriteQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize(Policy = "UserOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdFavouriteQuery() { Id = id,UserId = userId });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]   
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListFavouriteQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("my-favourite")]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeFavouriteQuery() {UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
