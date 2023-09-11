using OnlineShop.Application.Services;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Application;

public static class ServicesRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDatabaseService<Product>, ProductDatabaseService>();
        services.AddScoped<IDatabaseService<Review>, ReviewDatabaseService>();

        return services;
    }
}