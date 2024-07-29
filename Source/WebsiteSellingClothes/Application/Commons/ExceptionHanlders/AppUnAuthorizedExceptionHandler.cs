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
public class AppUnAuthorizedExceptionHandler : IExceptionHandler
{
	public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
	{
		if (exception is UnauthorizedAccessException)
		{
			var error = new ErrorDetailResponseDto()
			{
				Status = StatusCodes.Status403Forbidden,
				Type = exception.GetType().Name,
                Error = exception.Message,
				Instanse = "API",
				Title = "The API is not authorized by the server"

			};
            var errorEndPoint = error.GetType().GetProperties()
           .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(error));

            var response = JsonSerializer.Serialize(errorEndPoint);
			httpContext.Response.ContentType = "application/json";
			httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
			await httpContext.Response.WriteAsync(response, cancellationToken);
			return true;
		}
		return false;
	}
}
