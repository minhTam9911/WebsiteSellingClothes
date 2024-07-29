using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Commons.ExceptionHandlers;
public class AppIOExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is IOException)
		{
			var error = new ErrorDetailResponseDto()
			{
				Status = StatusCodes.Status503ServiceUnavailable,
				Type = exception.GetType().Name,
                Error = exception.Message,
				Instanse = "API",
				Title = "The server cannot process the uploaded file"

			};
            var errorEndPoint = error.GetType().GetProperties()
           .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(error));

            var response = JsonSerializer.Serialize(errorEndPoint);
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
			await httpContext.Response.WriteAsync(response, cancellationToken);
			return true;
		}
		return false;
	}
}
