using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Features.DiscountFeatures.Commands.Create;
using Application.Features.DiscountFeatures.Commands.Delete;
using Application.Features.DiscountFeatures.Commands.Update;
using Application.Features.DiscountFeatures.Queries.GetAll;
using Application.Features.DiscountFeatures.Queries.GetAllActive;
using Application.Features.DiscountFeatures.Queries.GetById;
using Application.Features.DiscountFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DiscountsController : ApiControllerBase
{
    /// <summary>
    ///  This is the API used to add discount data
    /// </summary>
    /// <param name="discountRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromBody] DiscountRequestDto discountRequestDto)
    {
        var result = await Sender.Send(new CreateDiscountCommand() { DiscountRequestDto = discountRequestDto });
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
    /// This is the API used to update discount data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="discountRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(string id,[FromBody] DiscountRequestDto discountRequestDto)
    {
        var result = await Sender.Send(new UpdateDiscountCommand() {Id = id, DiscountRequestDto = discountRequestDto });
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
    /// This is the API used to delete discount data
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Sender.Send(new DeleteDiscountCommand() { Id = id });
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
    /// This is the API used to get all discount data
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<DiscountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllDiscountQuery());
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is an API used to get data based on discount IDs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(DiscountResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await Meditor.Send(new GetByIdDiscountQuery() { Id = id });
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
    /// This is an API used to get discount data, filter and paginate it
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<DiscountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListDiscountQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is the API used to get data based on the discount status
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("status")]
    [ProducesResponseType(typeof(PagedListDto<DiscountResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery(Name = "pageIndex")] int pageIndex, [FromQuery(Name = "pageSize")] int pageSize)
    {
        var data = await Meditor.Send(new GetAllActiveDiscountQuery() { IsActive = isActive, PageIndex = pageIndex, PageSize = pageSize});
        return Ok(data);
    }
}
