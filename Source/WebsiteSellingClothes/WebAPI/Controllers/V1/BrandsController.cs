using Application.DTOs.Requests;
using Application.Features.BrandFeatures.Commands.Create;
using Application.Features.BrandFeatures.Commands.Delete;
using Application.Features.BrandFeatures.Commands.Update;
using Application.Features.BrandFeatures.Queries.GetAll;
using Application.Features.BrandFeatures.Queries.GetAllActive;
using Application.Features.BrandFeatures.Queries.GetById;
using Application.Features.BrandFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BrandsController : ApiControllerBase 
{
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] BrandRequestDto brandRequestDto)
    {
        var result = await Sender.Send(new CreateBrandCommand() { BrandRequestDto = brandRequestDto});
        if(result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id,[FromForm] BrandRequestDto brandRequestDto)
    {
        var result = await Sender.Send(new UpdateBrandCommand() {Id = id, BrandRequestDto = brandRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Sender.Send(new DeleteBrandCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllBrandQuery());
        if(data.Count > 0) return Ok(new { data = data});
        return BadRequest(new { data = data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdBrandQuery() {Id = id});
        if(data == null) return BadRequest(new {data = data});
        return Ok(new { data = data });
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListBrandQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery(Name = "pageIndex")] int pageIndex, [FromQuery(Name = "pageSize")] int pageSize )
    {
        var data = await Meditor.Send(new GetAllActiveBrandQuery() {
            IsActive = isActive,PageIndex = pageIndex, PageSize = pageSize});
        return Ok(data);
    }

}
