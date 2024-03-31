namespace InternetBank.Core.Application.Interfaces.Services.OperationServices;

public interface IOperationNotificationService
{
    Task NotifyAllClientsAsync(string message);
}
