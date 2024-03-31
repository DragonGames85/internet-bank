using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Infrastructure.Hubs.OperationHubs;

public class OperationHub : Hub
{
    public async Task SendOperationUpdate(string message)
    {
        await Clients.All.SendAsync("RecieveOperationUpdate", message);
    }
}
