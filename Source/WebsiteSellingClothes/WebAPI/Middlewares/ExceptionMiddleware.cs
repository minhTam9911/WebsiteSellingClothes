using Application.DTOs.Responses;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Middlewares;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class ExceptionMiddleware 
{
	private readonly RequestDelegate _next;

	public ExceptionMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await _next(httpContext);
		}
		catch(Exception ex)
		{
			await HandleExceptionAsync(httpContext, ex);
		}
		
	}

	private async Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
		context.Response.ContentType = "application/json";
		var error = new ErrorDetailResponseDto()
		{
			Status = context.Response.StatusCode,
			Type = "Server Error",
			Error = ex.Message,
			Instanse = "API",
			Title = "API Error"
		};
		var response = JsonSerializer.Serialize(error);
		await context.Response.WriteAsync(response);
	}
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class ExceptionMiddlewareExtensions
{
	public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ExceptionMiddleware>();
	}
}
