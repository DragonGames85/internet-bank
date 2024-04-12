namespace InternetBank.Core.Application.Interfaces.Services.OperationServices;

public interface IOperationNotificationService
{
    Task NotifyClientsAsync(string? receiverUserId = null, string? senderUserId = null);
}
