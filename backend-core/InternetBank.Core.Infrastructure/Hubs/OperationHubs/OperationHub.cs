using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace InternetBank.Core.Infrastructure.Hubs.OperationHubs;

public class OperationHub : Hub
{
    private static ConcurrentDictionary<string, string> usersConnections = new ConcurrentDictionary<string, string>();

    public override async Task OnConnectedAsync()
    {
        var httpContextFeature = Context.Features.Get<IHttpContextFeature>();
        var httpContext = httpContextFeature?.HttpContext;
        var userId = httpContext.Request.Query["userId"];

        if (!string.IsNullOrEmpty(userId))
        {
            usersConnections[userId] = Context.ConnectionId;
        }

        await Clients.Caller.SendAsync("ReceiveMessage", "You've connected");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userId = usersConnections.FirstOrDefault(x => x.Value == Context.ConnectionId).Key;

        if (userId != null)
        {
            usersConnections.TryRemove(userId, out _);
        }

        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendOperationUpdate(string message)
    {
        await Clients.All.SendAsync("RecieveOperationUpdate", message);
    }
    public async Task SendToUserOperationUpdate(string message, string userId)
    {
        if (usersConnections.TryGetValue(userId, out string connectionId))
        {
            await Clients.Client(connectionId).SendAsync("ReceiveMessage", message);
        }
    }

    public async Task SendMessage(string message)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "Ответ на ваше сообщение: " + message);
    }
}
