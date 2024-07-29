using Application.DTOs.Requests;
using Application.Features.ProductFeatures.Commands.Create;
using Application.Features.ProductFeatures.Commands.Delete;
using Application.Features.ProductFeatures.Commands.Update;
using Application.Features.ProductFeatures.Commands.UploadImage;
using Application.Features.ProductFeatures.Queries.GetAll;
using Application.Features.ProductFeatures.Queries.GetAllActive;
using Application.Features.ProductFeatures.Queries.GetByCode;
using Application.Features.ProductFeatures.Queries.GetById;
using Application.Features.ProductFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductsController : ApiControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] ProductRequestDto productRequestDto)
    {
        var result = await Sender.Send(new CreateProductCommand() { ProductRequestDto = productRequestDto });
        if (result == null) return BadRequest(result);
        return Ok(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductRequestDto productRequestDto)
    {
        var result = await Sender.Send(new UpdateProductCommand() { Id = id, ProductRequestDto = productRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("Upload/{id}")]
    public async Task<IActionResult> UploadImage(int id, [FromForm] IFormFile[] images)
    {
        var result = await Sender.Send(new UploadImageProductCommand() { Id = id, Images = images });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Sender.Send(new DeleteProductCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllProductQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdProductQuery() { Id = id });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [HttpGet("code/{code}")]
    public async Task<IActionResult> GetByCode(string code)
    {
        var data = await Meditor.Send(new GetByCodeProductQuery() { Code = code });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListProductQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetAllActiveProductQuery()
        {
            IsActive = isActive,
            FilterDto = filterDto
        });
        return Ok(data);
    }
}
