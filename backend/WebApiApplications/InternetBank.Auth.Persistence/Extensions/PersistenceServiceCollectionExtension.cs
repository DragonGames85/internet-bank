using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Persistence.Contexts.EfCore;
using InternetBank.Auth.Persistence.Contexts.EfCore.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Auth.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCoreMicrosoftSqlServerDbContext(configuration);
        services.AddRepositories();
    }

    private static void AddEfCoreMicrosoftSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var conStr = configuration["ConnectionStrings:InternetBankAuthDb"];
        services.AddSqlServer<ApplicationDbContext>(conStr);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
