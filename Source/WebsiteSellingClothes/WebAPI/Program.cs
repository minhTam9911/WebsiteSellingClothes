using Infrastructure;
using Application;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using Application.DTOs.Responses;
using WebAPI.Middlewares;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
//app.UseExceptionHandler(appError => {

//	appError.Run(async context =>
//	{
//		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//		context.Response.ContentType = "application/json";
//		var contextFeature  = context.Features.Get<IExceptionHandlerFeature>();
//		if (context != null)
//		{
//			await context.Response.WriteAsync(
//				new ServiceContainerResponseDto(
//					context.Response.StatusCode,
//					false,
//					"Internal Server Error"
//					).ToString()
//				) ;
//		}
//	});

//});

//app.UseExceptionHandler(_ => { });
//app.UseMiddleware<ExceptionMiddleware>();
app.UseApplication();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
