using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Services.AccountServices;
using InternetBank.Core.Infrastructure.Services.CurrencyServices;
using InternetBank.Core.Infrastructure.Services.OperationServices;
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
        services.AddTransient<IAccountGetService, AccountGetService>();
        services.AddTransient<IAccountHandleService, AccountHandleService>();
        services.AddTransient<ICurrencyGetService, CurrencyGetService>();
        services.AddTransient<ICurrencyHandleService, CurrencyHandleService>();
        services.AddTransient<IOperationGetService, OperationGetService>();
        services.AddTransient<IOperationHandleService, OperationHandleService>();
        // TODO: Add services
    }
}
