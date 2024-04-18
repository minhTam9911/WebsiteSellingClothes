
using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Exceptions;
public class AppNotImplementedExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is NotImplementedException)
		{
			var error = new ErrorDetailResponseDto()
			{
				Status = StatusCodes.Status501NotImplemented,
				Type = exception.GetType().ToString(),
				Message = exception.Message,
				Instanse = "API",
				Title = "The server does not support the requested method or requested functionality"

			};
			var response = JsonSerializer.Serialize(error);
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status501NotImplemented;
			await httpContext.Response.WriteAsync(response, cancellationToken);
			return true;
		}
		return false;
	}
}