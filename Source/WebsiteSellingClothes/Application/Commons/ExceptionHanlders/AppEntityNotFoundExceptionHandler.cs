using Application.Commons.Exceptions;
using Application.DTOs.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Application.Commons.ExceptionHanlders;
public class AppEntityNotFoundExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is EntityNotFoundException)
        {
            var error = new ErrorDetailResponseDto()
            {
                Status = StatusCodes.Status404NotFound,
                Type = exception.GetType().Name,
                Error = exception.Message,
                Instanse = "API",
                Title = "An error occurred ..."

            };
            var errorEndPoint = error.GetType().GetProperties()
           .ToDictionary(p => p.Name.ToLower(), p => p.GetValue(error));

            var response = JsonSerializer.Serialize(errorEndPoint);
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsync(response, cancellationToken);
            return true;
        }
        return false;
    }
}
