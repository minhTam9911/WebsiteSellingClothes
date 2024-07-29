using Application.DTOs.Requests;
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
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class FeedbacksController : ApiControllerBase
{
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] FeedbackRequestDto feedbackRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new CreateFeedbackCommand() { FeedbackRequestDto = feedbackRequestDto, UserId = userId });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] FeedbackRequestDto feedbackRequestDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new UpdateFeedbackCommand() { FeedbackRequestDto = feedbackRequestDto,UserId = userId, Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await Sender.Send(new DeleteFeedbackCommand() { Id = id, UserId = userId });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllFeedbackQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdFeedbackQuery() { Id = id });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListFeedbackQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("product")]
    public async Task<IActionResult> GetAllProduct([FromQuery] FilterDto filterDto, [FromQuery(Name = "productCode")] string productCode)
    {
        var data = await Meditor.Send(new GetAllForProductFeebackQuery() { Code = productCode, FilterDto = filterDto });
        return Ok(data);
    }
}
