using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Api;

public class TestOperationHub : Hub
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

    public async Task SendMessage(string user, string message)
    {
        await Clients.All.SendAsync("ReceiveMessage", user, message);
    }
}
