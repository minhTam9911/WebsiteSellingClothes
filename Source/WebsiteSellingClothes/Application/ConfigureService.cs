using Application.Commond.Exceptions;
using Application.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ConfigureService
{
	public static IServiceCollection AddApplication(this IServiceCollection services)
	{

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
		//	cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		});
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddExceptionHandler<AppNotImplementedExceptionHandler>();
		services.AddExceptionHandler<AppBadRequestExceptionHandler>();
		services.AddExceptionHandler<AppExceptionHandler>();
		services.AddExceptionHandler<AppIOExceptionHandler>();
		services.AddExceptionHandler<AppMethodNotAllowExceptionHandler>();
		services.AddExceptionHandler<AppUnAuthorizedExceptionHandler>();
		return services;
	}

	public static void UseApplication(this WebApplication app)
	{
		app.UseExceptionHandler(_ => { });
	}
}
