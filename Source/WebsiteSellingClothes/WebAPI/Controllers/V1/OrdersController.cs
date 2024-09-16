using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Features.OrderFeatures.Commands.Create;
using Application.Features.OrderFeatures.Queries.GetAll;
using Application.Features.OrderFeatures.Queries.GetAllForMe;
using Application.Features.OrderFeatures.Queries.GetById;
using Application.Features.OrderFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class OrdersController : ApiControllerBase
{
    /// <summary>
    /// This is the API used to get all order data
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    [ProducesResponseType(typeof(List<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllOrderQuery());
        return Ok( new { data = data });
    }
    /// <summary>
    /// This is an API used to get data based on order IDs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id) {

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdOrderQuery() { Id = id, UserId = userId });
        if (data == null) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    /// <summary>
    /// This is an API used to get order data, filter and paginate it
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery]FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListOrderQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is the API used to get my order data
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpGet("my-order")]
    [ProducesResponseType(typeof(PagedListDto<OrderResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllForMe([FromQuery]FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeOrderQuery() { UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
