using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Features.FeedbackFeatures.Commands.Create;
using Application.Features.FeedbackFeatures.Commands.Delete;
using Application.Features.FeedbackFeatures.Commands.Update;
using Application.Features.FeedbackFeatures.Queries.GetAll;
using Application.Features.FeedbackFeatures.Queries.GetAllForProduct;
using Application.Features.FeedbackFeatures.Queries.GetById;
using Application.Features.FeedbackFeatures.Queries.GetList;
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
public class FeedbacksController : ApiControllerBase
{
    /// <summary>
    /// This is the API used to add feedback data
    /// </summary>
    /// <param name="feedbackRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromBody] FeedbackRequestDto feedbackRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new CreateFeedbackCommand() { FeedbackRequestDto = feedbackRequestDto, UserId = userId });
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
    /// This is the API used to update feedback data
    /// </summary>
    /// <param name="id"></param>
    /// <param name="feedbackRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] FeedbackRequestDto feedbackRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new UpdateFeedbackCommand() { FeedbackRequestDto = feedbackRequestDto, UserId = userId, Id = id });
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
    /// This is the API used to delete feedback
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = "UserOnly")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new DeleteFeedbackCommand() { Id = id, UserId = userId });
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
    /// This is the API used to get all feedback data
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllFeedbackQuery());
        return Ok(new { data = data });

    }

    /// <summary>
    /// This is an API used to get data based on feedback IDs
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(FeedbackResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdFeedbackQuery() { Id = id });
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
    /// This is an API used to get feedback data, filter and paginate it
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(PagedListDto<FeedbackResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListFeedbackQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is an API used to get feedback data on productCode, filter and paginate it.
    /// </summary>
    /// <param name="filterDto"></param>
    /// <param name="productCode"></param>
    /// <returns></returns>
    [HttpGet("product")]
    public async Task<IActionResult> GetAllProduct([FromQuery] FilterDto filterDto, [FromQuery(Name = "productCode")] string productCode)
    {
        var data = await Meditor.Send(new GetAllForProductFeebackQuery() { Code = productCode, FilterDto = filterDto });
        return Ok(data);
    }
}
