using SweetBakery.Interfaces;
using SweetBakery.Services;

namespace SweetBakery.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        services.AddControllers();

        services.AddScoped<IProductService, ProductService>();

        return services;
    }
}