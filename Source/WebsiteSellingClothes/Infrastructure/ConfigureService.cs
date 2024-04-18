using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		var connectionString = configuration["ConnectionStrings:Default"];
		services.AddDbContext<AppDbContext>(options =>options.UseLazyLoadingProxies().UseSqlServer(connectionString));
		services.AddScoped<IRoleRepository, RoleRepository>();
		return services;
	}
}
