using OnlineShop.Application.Configurations;
using OnlineShop.Application.Models;
using OnlineShop.Application.Repositories;
using OnlineShop.Application.Services;
using OnlineShop.Domain.Models;
using OnlineShop.Domain.Services;

namespace OnlineShop.Application;

public static class ServicesRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<BlobStorageConfiguration>().Bind(configuration.GetSection(nameof(BlobStorageConfiguration)));
        services.AddScoped<IFilteringOptions, FilteringOptions>();
        services.AddScoped<IRepository<Dish>, DishRepository>();
        services.AddScoped<IRepository<Review>, ReviewRepository>();
        services.AddScoped<IRepository<Category>, CategoryRepository>();
        services.AddScoped<FileService>();
        services.AddScoped<DishService>();

        return services;
    }
}