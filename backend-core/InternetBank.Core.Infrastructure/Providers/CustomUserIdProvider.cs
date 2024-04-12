using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Infrastructure.Providers;

public class CustomUserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("userId")?.Value ?? string.Empty;
    }
}
