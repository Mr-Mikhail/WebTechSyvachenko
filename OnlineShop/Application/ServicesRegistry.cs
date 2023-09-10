using OnlineShop.Application.Services;
using OnlineShop.Domain.Services;

namespace OnlineShop.Application;

public static class ServicesRegistry
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IProductManagementService, ProductManagementService>();

        return services;
    }
}