using Microsoft.AspNetCore.Http.Connections.Features;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace InternetBank.Core.Infrastructure.Hubs.EmployeeOperationHubs;

public class EmployeeOperationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "You've connected");
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        await base.OnDisconnectedAsync(exception);
    }

    public async Task SendOperationUpdate(string message)
    {
        await Clients.All.SendAsync("RecieveOperationUpdate", message);
    }

    public async Task SendMessage(string message)
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "Ответ на ваше сообщение: " + message);
    }
}
