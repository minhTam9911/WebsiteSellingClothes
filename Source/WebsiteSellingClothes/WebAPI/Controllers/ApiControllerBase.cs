using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
public class ApiControllerBase : ControllerBase
{
	private ISender sender;
	protected ISender Sender => sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
	private IMediator mediator;
	protected ISender Meditor => mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();

	
}
