using Application.DTOs.Requests;
using Application.DTOs.Responses;
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
using System.Net;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FavouritesController : ApiControllerBase
{
    /// <summary>
    /// This is the API used to add favourite data
    /// </summary>
    /// <param name="favouriteRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromBody] FavouriteRequestDto favouriteRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new CreateFavouriteCommand() { FavouriteRequestDto = favouriteRequestDto, UserId = userId });
        if (result.Flag) return Ok(result);
        return BadRequest(new ErrorValidationResponse()
        {
            Errors = result.Message,
            Status = (int)HttpStatusCode.BadRequest,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "BadRequestExeption"
        });
    }

    /// <summary>
    /// This is the API used to delete favourite
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new DeleteFavouriteCommand() { UserId = userId, Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(new ErrorValidationResponse()
        {
            Errors = result.Message,
            Status = (int)HttpStatusCode.NotFound,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "BadRequestExeption"
        });
    }

    /// <summary>
    /// This is the API used to get all favourite data
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    [ProducesResponseType(typeof(List<FavourireResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllFavouriteQuery());
        return Ok(new { data = data });

    }

    /// <summary>
    /// This is an API used to get data based on favourite IDs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(FavourireResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdFavouriteQuery() { Id = id, UserId = userId });
        if (data == null) return NotFound(new ErrorValidationResponse()
        {
            Errors = "Data does not exist",
            Status = (int)HttpStatusCode.NotFound,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "NotFoundException"
        });
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is an API used to get contact data, filter and paginate it
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<FavourireResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListFavouriteQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is the API used to get my favorite data
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [HttpGet("my-favourite")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PagedListDto<FavourireResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeFavouriteQuery() { UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
