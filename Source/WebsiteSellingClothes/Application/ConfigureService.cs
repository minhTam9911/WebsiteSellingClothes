using Application.Commons.Behaviours;
using Application.Commons.ExceptionHandlers;
using Application.Commons.ExceptionHanlders;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ConfigureService
{
	public static IServiceCollection AddApplication(this IServiceCollection services,IConfiguration configuration)
	{
		services.AddHttpContextAccessor();
		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
			cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		});
		
		services.AddExceptionHandler<AppNotImplementedExceptionHandler>();
		services.AddExceptionHandler<AppBadRequestExceptionHandler>();
		services.AddExceptionHandler<AppExceptionHandler>();
		services.AddExceptionHandler<AppIOExceptionHandler>();
        services.AddExceptionHandler<AppEntityNotFoundExceptionHandler>();
        services.AddExceptionHandler<AppMethodNotAllowExceptionHandler>();
		services.AddExceptionHandler<AppUnAuthorizedExceptionHandler>();
        services.AddControllers()
                .AddFluentValidation(options =>
                {
                    FluentValidationMvcConfiguration fluentValidationMvcConfiguration = options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
                });

        services.AddFluentValidationAutoValidation();

        return services;
	}

	public static void UseApplication(this WebApplication app)
	{
		app.UseExceptionHandler(_ => { });
	}
}
