using InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using InternetBank.Auth.Infrastructure.Services.JwtServices;
using InternetBank.Auth.Infrastructure.Services.RoleServices;
using InternetBank.Auth.Infrastructure.Services.SettingsServices;
using InternetBank.Auth.Infrastructure.Services.UserServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InternetBank.Auth.Infrastructure.Extensions;

public static class InfrastructureServiceCollectionExtension
{
    public static void AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddServices();
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<IUserAuthService, UserAuthService>();
        services.AddTransient<IUserGetService, UserGetService>();
        services.AddTransient<IUserHandleService, UserHandleService>();
        services.AddTransient<IRoleGetService, RoleGetService>();
        services.AddTransient<IRoleHandleService, RoleHandleService>();
        services.AddTransient<ISettingsGetService, SettingsGetService>();
        services.AddTransient<ISettingsHandleService, SettingsHandleService>();
        services.AddTransient<IJwtTokenService, JwtTokenService>();
    }
}
