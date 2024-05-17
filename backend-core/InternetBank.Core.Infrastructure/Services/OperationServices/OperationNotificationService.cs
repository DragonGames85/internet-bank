using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Hubs.OperationHubs;
using Microsoft.AspNetCore.SignalR;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationNotificationService : IOperationNotificationService
{
    private readonly IHubContext<OperationHub> _hubContext;
    private readonly IServiceProvider _serviceProvider;

    public OperationNotificationService(IHubContext<OperationHub> hubContext, IServiceProvider serviceProvider)
    {
        _hubContext = hubContext;
        _serviceProvider = serviceProvider;
    }

    public async Task NotifyClientsAsync(string? receiverUserId = null, string? senderUserId = null)
    {
        var hub = (OperationHub)_serviceProvider.GetService(typeof(OperationHub));
        if (hub != null)
        {
            if (receiverUserId != null)
                await hub.SendToUserOperationUpdate("ReceiveOperationsUpdate", receiverUserId);
            if (senderUserId != null)
                await hub.SendToUserOperationUpdate("ReceiveOperationsUpdate", senderUserId);
        }
    }
}
