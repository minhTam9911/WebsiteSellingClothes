using Application.DTOs.Requests;
using Application.DTOs.Responses;
using Application.Features.CategoryFeatures.Commands.Create;
using Application.Features.CategoryFeatures.Commands.Delete;
using Application.Features.CategoryFeatures.Commands.Update;
using Application.Features.CategoryFeatures.Queries.GetAll;
using Application.Features.CategoryFeatures.Queries.GetAllActive;
using Application.Features.CategoryFeatures.Queries.GetById;
using Application.Features.CategoryFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CategoriesController : ApiControllerBase
{

    /// <summary>
    /// This is the API used to add new data to the category
    /// </summary>
    /// <param name="categoryRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Insert([FromForm] CategoryRequestDto categoryRequestDto)
    {
        var result = await Sender.Send(new CreateCategoryCommand() { CategoryRequestDto = categoryRequestDto });
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
    /// This is the API used to update the data for the category
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoryRequestDto"></param>
    /// <returns></returns>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ServiceContainerResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromForm] CategoryRequestDto categoryRequestDto)
    {
        var result = await Sender.Send(new UpdateCategoryCommand() { Id = id, CategoryRequestDto = categoryRequestDto });
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
    /// This is the API used to delete data for the category
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
        var result = await Sender.Send(new DeleteCategoryCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(new ErrorValidationResponse()
        {
            Errors = result.Message,
            Status = (int)HttpStatusCode.NotFound,
            Title = "Client",
            TraceId = Guid.NewGuid().ToString(),
            Type = "BadRequestExeption"
        });
    }

    /// <summary>
    /// This is the API used to get all the data for the category
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllCategoryQuery());
        return Ok(new { data = data });
    }


    /// <summary>
    /// This is the API used to get 1 data based on the ID for the category
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CategoryResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdCategoryQuery() { Id = id });
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
    /// This is the API used to get all the data for the category, filter and paginate them
    /// </summary>
    /// <param name="filterDto"></param>
    /// <returns></returns>
    [HttpGet("filter")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedListDto<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListCategoryQuery() { FilterDto = filterDto });
        return Ok(data);
    }


    /// <summary>
    /// This is the API used to retrieve all data based on state
    /// </summary>
    /// <param name="isActive"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    [HttpGet("status")]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(PagedListDto<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorValidationResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllActive([FromQuery(Name = "isActive")] bool isActive, [FromQuery(Name = "pageIndex")] int pageIndex, [FromQuery(Name = "pageSize")] int pageSize)
    {
        var data = await Meditor.Send(new GetAllActiveCategoryQuery() { IsActive = isActive, PageIndex = pageIndex, PageSize = pageSize });
        return Ok(data);
    }
}
