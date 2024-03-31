using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Hubs.OperationHubs;
using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationNotificationService : IOperationNotificationService
{
    private readonly IHubContext<OperationHub> _hubContext;

    public OperationNotificationService(IHubContext<OperationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyAllClientsAsync(string message)
    {
        await _hubContext.Clients.All.SendAsync("ReceiveOperationsUpdate", message);
    }
}
