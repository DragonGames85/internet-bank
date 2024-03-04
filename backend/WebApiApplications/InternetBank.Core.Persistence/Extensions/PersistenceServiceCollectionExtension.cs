using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Core.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
    }

    public static void AddServices(this IServiceCollection services)
    {
        // TODO: Add services
    }
}
