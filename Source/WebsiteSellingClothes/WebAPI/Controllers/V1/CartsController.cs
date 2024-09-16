using Application.DTOs.Requests;
using Application.DTOs.Responses;
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
using System.Net;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CartsController : ApiControllerBase
{

    /// <summary>
    /// This is the API used to add new data to the cart
    /// </summary>
    /// <param name="cartRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromBody] CartRequestDto cartRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new CreateCartCommand() { UserId = userId, CartRequestDto = cartRequestDto });
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
    /// This is the API used to update the data for the cart
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cartRequestDto"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id,[FromBody] CartRequestDto cartRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new UpdateCartCommand() { UserId = userId, CartRequestDto = cartRequestDto,Id = id });
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
    /// This is the API used to delete data for the cart
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var result = await Sender.Send(new DeleteCartCommand() { UserId = userId, Id = id });
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
    /// This is the API used to get all the data for the cart
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    [ProducesResponseType(typeof(List<CartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllCartQuery());
        return Ok(new { data = data });

    }

    /// <summary>
    /// This is the API used to get all of the user's data for the cart, filter and paginate them
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpGet("my-cart")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(PagedListDto<CartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var data = await Meditor.Send(new GetAllForMeCartQuery() { UserId = userId, FilterDto = filterDto});
        return Ok(data);
    }

    /// <summary>
    /// This is the API used to get all the data for the cart, filter and paginate them
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<CartResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListCartQuery() {FilterDto = filterDto });
        return Ok(data);
    }


    /// <summary>
    /// This is the API used to get 1 data based on the ID for the cart
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(CartResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdCartQuery() {Id = id,UserId =userId});
        if (data == null) return NotFound(new  ErrorValidationResponse()
        {
            Errors = "Data does not exist",
            Status = (int)HttpStatusCode.NotFound,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "BadRequestExeption"
        });
        return Ok(new { data = data });
    }
}
