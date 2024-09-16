using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class ConfigureService
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:Default"];
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseLazyLoadingProxies().UseSqlServer(connectionString);
        });
       
        services.AddScoped<IAuthRepository, AuthReposiotry>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ICartRepository, CartRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        services.AddScoped<IFavouriteRepository, FavouriteRepository>();
        services.AddScoped<IFeedbackRepository, FeedbackRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderReposiroty>();

        services.AddScoped<IProductImageRepository, ProductImageRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IMerchantRepository, MerchantRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<IPaymentNotificationRepository, PaymentNotificationRepository>();
        services.AddScoped<IPaymentSignatureRepository, PaymentSignatureRepository>();
        services.AddScoped<IPaymentDestinationRepository, PaymentDestinationRepository>();
        services.AddScoped<IPaymentTransactionRepository, PaymentTransactionRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}
