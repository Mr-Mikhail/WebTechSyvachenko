using OnlineShop.Application.Repositories;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Application;

public static class ServicesRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IRepository<Dish>, ProductRepository>();
        services.AddScoped<IRepository<Review>, ReviewRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();

        return services;
    }
}