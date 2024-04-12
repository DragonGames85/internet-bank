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

    public async Task NotifyClientsAsync(string? receiverUserId = null, string? senderUserId = null)
    {
        if (receiverUserId != null)
            await _hubContext.Clients.User(receiverUserId).SendAsync("ReceiveOperationsUpdate");
        if (senderUserId != null)
            await _hubContext.Clients.User(senderUserId).SendAsync("ReceiveOperationsUpdate");
    }
}
