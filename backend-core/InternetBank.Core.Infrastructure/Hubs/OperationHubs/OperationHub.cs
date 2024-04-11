using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Infrastructure.Hubs.OperationHubs;

public class OperationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.Caller.SendAsync("ReceiveMessage", "You've connected");
        await base.OnConnectedAsync();
    }

    public async Task SendOperationUpdate(string message)
    {
        await Clients.All.SendAsync("RecieveOperationUpdate", message);
    }
}
