
using Application.DTOs.Requests;
using Application.Features.ProductImageFeatures.Commands.Create;
using Application.Features.ProductImageFeatures.Commands.Delete;
using Application.Features.ProductImageFeatures.Commands.Update;
using Application.Features.ProductImageFeatures.Queries.GetAll;
using Application.Features.ProductImageFeatures.Queries.GetById;
using Application.Features.ProductImageFeatures.Queries.GetByProductCode;
using Application.Features.ProductImageFeatures.Queries.GetByProductId;
using Application.Features.ProductImageFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ProductImagesController : ApiControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] ProductImageRequestDto productImageRequestDto)
    {
        var result = await Sender.Send(new CreateProductImageCommand() { ProductImageRequestDto = productImageRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] ProductImageRequestDto productImageRequestDto)
    {
        var result = await Sender.Send(new UpdateProductImageCommand() { Id = id, ProductImageRequestDto = productImageRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Sender.Send(new DeleteProductImageCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllProductImageQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdProductImageQuery() { Id = id });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListProductImageQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [HttpGet("product-code/{code}")]
    public async Task<IActionResult> GetByProductCode(string code)
    {
        var data = await Meditor.Send(new GetByProductCodeProductImageQuery()
        {
           ProductCode = code
        });
        return Ok(data);
    }

    [HttpGet("product-id/{id}")]
    public async Task<IActionResult> GetByProductId(int id)
    {
        var data = await Meditor.Send(new GetByProductIdProductImageQuery()
        {
            ProductId = id
        });
        return Ok(data);
    }

}
