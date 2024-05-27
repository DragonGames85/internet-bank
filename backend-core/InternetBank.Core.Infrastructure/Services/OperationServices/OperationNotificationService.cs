using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Hubs.EmployeeOperationHubs;
using InternetBank.Core.Infrastructure.Hubs.OperationHubs;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Json;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationNotificationService : IOperationNotificationService
{
    private readonly IHubContext<OperationHub> _hubContext;
    private readonly IHubContext<EmployeeOperationHub> _employeeHubContext;
    private readonly IServiceProvider _serviceProvider;
    private readonly IHttpClientFactory _httpClientFactory;

    public OperationNotificationService(IHubContext<OperationHub> hubContext, IHubContext<EmployeeOperationHub> employeeHubContext, IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
    {
        _hubContext = hubContext;
        _employeeHubContext = employeeHubContext;
        _serviceProvider = serviceProvider;
        _httpClientFactory = httpClientFactory;
    }

    public async Task NotifyClientsAsync(string? receiverUserId = null, string? senderUserId = null)
    {
        var hub = (OperationHub)_serviceProvider.GetService(typeof(OperationHub));
        var employeeHub = (EmployeeOperationHub)_serviceProvider.GetService(typeof(EmployeeOperationHub));
        
        if (hub != null)
        {
            if (receiverUserId != null)
                await hub.SendToUserOperationUpdate("ReceiveOperationsUpdate", receiverUserId);
            if (senderUserId != null)
                await hub.SendToUserOperationUpdate("ReceiveOperationsUpdate", senderUserId);
        }

        if (employeeHub != null)
        {
            await employeeHub.SendOperationUpdate("ReceiveOperationsUpdate");
        }

        var httpClient = _httpClientFactory.CreateClient();
        var authAppUrl = "https://bayanshonhodoev.ru/auth";
        var coreAppUrl = "https://bayanshonhodoev.ru/core";

        if (receiverUserId != null)
        {
            var response = await httpClient.GetAsync($"{authAppUrl}/api/Device/user/{receiverUserId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var devices = JsonConvert.DeserializeObject<List<Device>>(jsonContent);

                foreach (var device in devices)
                {
                    var notification = new Notification()
                    {
                        Token = device.Token,
                        Message = "Создана новая операция"
                    };

                    var content = new StringContent(string.Empty);

                    await httpClient.PostAsync($"{coreAppUrl}/sendNotification?token={notification.Token}&message={notification.Message}", content);
                }
            }
        }

        if (senderUserId != null)
        {
            var response = await httpClient.GetAsync($"{authAppUrl}/api/Device/user/{senderUserId}");

            if (response.IsSuccessStatusCode)
            {
                var jsonContent = await response.Content.ReadAsStringAsync();
                var devices = JsonConvert.DeserializeObject<List<Device>>(jsonContent);

                foreach (var device in devices)
                {
                    var notification = new Notification()
                    {
                        Token = device.Token,
                        Message = "Создана новая операция"
                    };

                    var content = new StringContent(string.Empty);

                    await httpClient.PostAsync($"{coreAppUrl}/sendNotification?token={notification.Token}&message={notification.Message}", content);
                }
            }
        }

        var responseEmployee = await httpClient.GetAsync($"{authAppUrl}/api/Device/employees");

        if (responseEmployee.IsSuccessStatusCode)
        {
            var jsonContent = await responseEmployee.Content.ReadAsStringAsync();
            var devices = JsonConvert.DeserializeObject<List<Device>>(jsonContent);

            foreach (var device in devices)
            {
                var notification = new Notification()
                {
                    Token = device.Token,
                    Message = "Создана новая операция клиента"
                };

                var content = new StringContent(string.Empty);

                await httpClient.PostAsync($"{coreAppUrl}/sendNotification?token={notification.Token}&message={notification.Message}", content);
            }
        }
    }
}

public class Notification
{
    public string Token { get; set; }
    public string Message { get; set; }
}

public class Device
{
    public string Token { get; set; }
}
