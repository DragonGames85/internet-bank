using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreditService.Services
{
    public class MyHostedService : BackgroundService
    {
     
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                   
                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }
    }
}
