using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Commond.Exceptions;
public class AppMethodNotAllowExceptionHandler :  IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
{
	if (exception is IOException)
	{
		var error = new ErrorDetailResponseDto()
		{
			Status = StatusCodes.Status405MethodNotAllowed,
			Type = exception.GetType().ToString(),
			Message = exception.Message,
			Instanse = "API",
			Title = "The server cannot process the uploaded file"

		};
		var response = JsonSerializer.Serialize(error);
		httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status405MethodNotAllowed;
			await httpContext.Response.WriteAsync(response, cancellationToken);
		return true;
	}
	return false;
}

}
