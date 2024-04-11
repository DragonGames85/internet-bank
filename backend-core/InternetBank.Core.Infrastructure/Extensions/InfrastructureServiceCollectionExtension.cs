using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Refit.Interfaces.Cbr;
using InternetBank.Core.Infrastructure.Services.AccountServices;
using InternetBank.Core.Infrastructure.Services.CurrencyServices;
using InternetBank.Core.Infrastructure.Services.OperationServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace InternetBank.Core.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtension
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
        services.AddWebSockets();
        services.AddMessageQueue();
        services.AddCbrClient();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAccountGetService, AccountGetService>();
        services.AddTransient<IAccountHandleService, AccountHandleService>();
        services.AddTransient<ICurrencyGetService, CurrencyGetService>();
        services.AddTransient<ICurrencyHandleService, CurrencyHandleService>();
        services.AddTransient<IOperationGetService, OperationGetService>();
        services.AddTransient<IOperationHandleService, OperationHandleService>();
        // TODO: Add services
    }

    private static void AddCbrClient(this IServiceCollection services)
    {
        var refitSettings = new RefitSettings();

        services.AddRefitClient<ICbrClient>(refitSettings)
            .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://www.cbr-xml-daily.ru"));
    }

    private static void AddWebSockets(this IServiceCollection services)
    {
        services.AddSingleton<IOperationNotificationService, OperationNotificationService>();
    }

    private static void AddMessageQueue(this IServiceCollection services)
    {
        services.AddHostedService<OperationBackgroundService>();
    }
}
