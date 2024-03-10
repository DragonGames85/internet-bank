using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CreditService.Services
{
    public class MyHostedService : IHostedService
    {
        private readonly IPaymentService _paymentService;
        public MyHostedService(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                  Console.WriteLine(DateTime.Now.ToString());
                    
                    await Task.Delay(new TimeSpan(0, 1, 0));
                    await _paymentService.CheckPaymentDay();
                    // 5 second delay
                }
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
