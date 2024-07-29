using Application.DTOs.Requests;
using Application.Features.OrderDetailFeatures.Commands.Create;
using Application.Features.OrderDetailFeatures.Queries.GetAll;
using Application.Features.OrderDetailFeatures.Queries.GetAllForMe;
using Application.Features.OrderDetailFeatures.Queries.GetByIdForMe;
using Application.Features.OrderDetailFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class OrderDetailsController : ApiControllerBase
{

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllOrderDetailQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListOrderDetailQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetByIdOrderDetailQuery() { Id = id, UserId = userId });
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }

    [Authorize(Policy = "UserOnly")]
    [HttpGet("my-order-detail")]
    public async Task<IActionResult> GetAllForMe([FromQuery] FilterDto filterDto)
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var data = await Meditor.Send(new GetAllForMeOrderDetailQuery() {UserId = userId, FilterDto = filterDto });
        return Ok(data);
    }
}
