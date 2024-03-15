using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace InternetBank.Auth.Application.Extensions;

public static class ApplicationServiceCollectionExtension
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddMediator();
    }

    private static void AddMediator(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
