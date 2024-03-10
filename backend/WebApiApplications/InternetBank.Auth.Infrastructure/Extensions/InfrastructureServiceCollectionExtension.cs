using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using InternetBank.Auth.Infrastructure.Services.AccountServices;
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
        services.AddTransient<IUserAuthService, UserAuthService>();
        services.AddTransient<IUserHandleService, UserHandleService>();
        // TODO: Add services
    }
}
