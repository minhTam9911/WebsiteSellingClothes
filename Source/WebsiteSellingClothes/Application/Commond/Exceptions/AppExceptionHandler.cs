using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Commond.Exceptions;
public class AppExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is Exception)
		{
			var error = new ErrorDetailResponseDto()
			{
				Status = StatusCodes.Status500InternalServerError,
				Type = exception.GetType().ToString(),
				Message = exception.Message,
				Instanse = "API",
				Title = "The server encountered an unexpected problem and could not process the request"

			};
			var response = JsonSerializer.Serialize(error);
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
			await httpContext.Response.WriteAsync(response, cancellationToken);
			return true;
		}
		return false;
	}
}
