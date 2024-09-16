using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Features.OrderDetailFeatures.Commands.Create;
using Application.Features.OrderDetailFeatures.Queries.GetAll;
using Application.Features.OrderDetailFeatures.Queries.GetAllForMe;
using Application.Features.OrderDetailFeatures.Queries.GetByIdForMe;
using Application.Features.OrderDetailFeatures.Queries.GetList;
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
public class OrderDetailsController : ApiControllerBase
{
    /// <summary>
    /// This is the API used to get all order detail data
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    [ProducesResponseType(typeof(List<OrderDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllOrderDetailQuery());
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is an API used to get order detail data, filter and paginate it
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<OrderDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListOrderDetailQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is an API used to get data based on order detail IDs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(OrderDetailResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdOrderDetailQuery() { Id = id, UserId = userId });
        if (data == null) return NotFound(new ErrorValidationResponse()
        {
            Errors = "Data does not exist",
            Status = (int)HttpStatusCode.NotFound,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "BadRequestExeption"
        });
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is the API used to get my order detail data
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpGet("my-order-detail")]
    [ProducesResponseType(typeof(PagedListDto<OrderDetailResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeOrderDetailQuery() {UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
