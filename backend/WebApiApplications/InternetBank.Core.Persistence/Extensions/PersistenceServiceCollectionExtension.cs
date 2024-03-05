using InternetBank.Core.Persistence.Contexts.EfCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Core.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCoreMicrosoftSqlServerDbContext(configuration);
    }

    public static void AddEfCoreMicrosoftSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var conStr = configuration["ConnectionStrings:InternetBankDb"];
        services.AddSqlServer<ApplicationDbContext>(conStr);
    }
}
