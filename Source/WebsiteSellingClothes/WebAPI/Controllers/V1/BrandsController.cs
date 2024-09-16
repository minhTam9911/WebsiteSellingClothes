using Application.DTOs.Requests;
using Application.DTOs.Responses;
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
using System.Net;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class BrandsController : ApiControllerBase
{
    /// <summary>
    /// This is the API used to create a new brand
    /// </summary>
    /// <param name="brandRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromForm] BrandRequestDto brandRequestDto)
    {
        var result = await Sender.Send(new CreateBrandCommand() { BrandRequestDto = brandRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// This is the API used to update the brand
    /// </summary>
    /// <param name="id"></param>
    /// <param name="brandRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromForm] BrandRequestDto brandRequestDto)
    {
        var result = await Sender.Send(new UpdateBrandCommand() { Id = id, BrandRequestDto = brandRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// This is the API used to delete the brand
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Sender.Send(new DeleteBrandCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    /// <summary>
    /// This is the API used to get all the data of brands
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<BrandResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllBrandQuery());
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is the API used to get the data of a brand
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(BrandResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdBrandQuery() { Id = id });
        if (data == null) return NotFound(new ErrorValidationResponse() { 
            Errors = "Data does not exist", 
            Status = (int)HttpStatusCode.NotFound, 
            Title = "Client", TraceId = Guid.NewGuid().ToString(), 
            Type = "BadRequestExeption"});
        return Ok(new { data = data });
    }

    /// <summary>
    /// This is an API used to filter data as well as paginate for brands
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedListDto<BrandResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListBrandQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    /// <summary>
    /// This is an API used to get data on brands based on their status and paginate them
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("status")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedListDto<BrandResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery(Name = "pageIndex")] int pageIndex, [FromQuery(Name = "pageSize")] int pageSize)
    {
        var data = await Meditor.Send(new GetAllActiveBrandQuery()
        {
            IsActive = isActive,
            PageIndex = pageIndex,
            PageSize = pageSize
        });
        return Ok(data);
    }

}
