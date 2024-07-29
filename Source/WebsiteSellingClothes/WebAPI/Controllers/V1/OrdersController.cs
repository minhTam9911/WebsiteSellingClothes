using Application.DTOs.Requests;
using Application.Features.OrderFeatures.Commands.Create;
using Application.Features.OrderFeatures.Queries.GetAll;
using Application.Features.OrderFeatures.Queries.GetAllForMe;
using Application.Features.OrderFeatures.Queries.GetById;
using Application.Features.OrderFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class OrdersController : ApiControllerBase
{
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllOrderQuery());
        if(data.Count>0) return Ok( new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id) {

        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdOrderQuery() { Id = id, UserId = userId });
        if (data == null) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery]FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListOrderQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [Authorize(Policy = "UserOnly")]
    [HttpGet("my-order")]
    public async Task<IActionResult> GetAllForMe([FromQuery]FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeOrderQuery() { UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
