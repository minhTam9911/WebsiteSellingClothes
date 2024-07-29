using Application.DTOs.Requests;
using Application.Features.BrandFeatures.Commands.Create;
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

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class DiscountsController : ApiControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] DiscountRequestDto discountRequestDto)
    {
        var result = await Sender.Send(new CreateDiscountCommand() { DiscountRequestDto = discountRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id,[FromBody] DiscountRequestDto discountRequestDto)
    {
        var result = await Sender.Send(new UpdateDiscountCommand() {Id = id, DiscountRequestDto = discountRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await Sender.Send(new DeleteDiscountCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllDiscountQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var data = await Meditor.Send(new GetByIdDiscountQuery() { Id = id });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListDiscountQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery(Name = "pageIndex")] int pageIndex, [FromQuery(Name = "pageSize")] int pageSize)
    {
        var data = await Meditor.Send(new GetAllActiveDiscountQuery() { IsActive = isActive, PageIndex = pageIndex, PageSize = pageSize});
        return Ok(data);
    }
}
