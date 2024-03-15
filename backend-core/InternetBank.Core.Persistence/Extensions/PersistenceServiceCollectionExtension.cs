using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;
using InternetBank.Core.Persistence.Contexts.EfCore;
using InternetBank.Core.Persistence.Contexts.EfCore.Repositories;
using InternetBank.Core.Persistence.Contexts.EfCore.Repositories.AccountRepositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Core.Persistence.Extensions;

public static class PersistenceServiceCollectionExtension
{
    public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddEfCoreMicrosoftSqlServerDbContext(configuration);
        services.AddRepositories();
    }

    private static void AddEfCoreMicrosoftSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var conStr = configuration["ConnectionStrings:InternetBankDb"];
        services.AddSqlServer<ApplicationDbContext>(conStr);
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>))
            .AddTransient<IAccountRepository, AccountRepository>();

    }
}
