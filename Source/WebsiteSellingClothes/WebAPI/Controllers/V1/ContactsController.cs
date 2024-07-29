using Application.DTOs.Requests;
using Application.Features.ContactFeatures.Commands.Create;
using Application.Features.ContactFeatures.Commands.Delete;
using Application.Features.ContactFeatures.Queries.GetAll;
using Application.Features.ContactFeatures.Queries.GetById;
using Application.Features.ContactFeatures.Queries.GetList;
using Asp.Versioning;
using Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1;
[ApiVersion(1)]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class ContactsController : ApiControllerBase
{
    [Authorize(Policy = "UserOnly")]
    [HttpPost]
    public async Task<IActionResult> Insert([FromBody] ContactRequestDto contactRequestDto)
    {
        var result = await Sender.Send(new CreateContactCommand() { ContactRequestDto = contactRequestDto });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpPost("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await Sender.Send(new DeleteContactCommand() { Id = id });
        if (result.Flag) return Ok(result);
        return BadRequest(result);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("filter")]
    public async Task<IActionResult> GetList([FromQuery] FilterDto filterDto)
    {
        var data = await Meditor.Send(new GetListContactQuery() { FilterDto = filterDto });
        return Ok(data);
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await Meditor.Send(new GetAllContactQuery());
        if (data.Count > 0) return Ok(new { data = data });
        return BadRequest(new { data = data });
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var data = await Meditor.Send(new GetByIdContactQuery() { Id = id});
        if (data == null) return BadRequest(new { data = data });
        return Ok(new { data = data });
    }
}
