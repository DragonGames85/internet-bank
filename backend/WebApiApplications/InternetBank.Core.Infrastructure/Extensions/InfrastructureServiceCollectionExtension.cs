using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Core.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtension
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
    }

    public static void AddServices(this IServiceCollection services)
    {
        // TODO: Add services
    }
}
