using Application.Commons.Exceptions;
using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application.Commons.ExceptionHanlders;
public class AppJwtTokenInvalidException : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is JwtTokenInvalidException)
        {
            var error = new ErrorDetailResponseDto()
            {
                Status = StatusCodes.Status401Unauthorized,
                Type = exception.GetType().Name,
                Error = exception.Message,
                Instanse = "API",
                Title = "The api request sent failed"

            };
            var errorEndPoint = error.GetType().GetProperties()
            .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(error));

            var response = JsonSerializer.Serialize(errorEndPoint);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            await httpContext.Response.WriteAsync(response, cancellationToken);
            return true;
        }
        return false;
    }
}

