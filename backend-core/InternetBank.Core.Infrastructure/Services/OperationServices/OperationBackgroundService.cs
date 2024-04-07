using Microsoft.Extensions.Hosting;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Application.DTOs.OperationDTOs;
using System.Text.Json;
using RabbitMQ.Client.Exceptions;

namespace InternetBank.Core.Infrastructure.Services.OperationServices;

public class OperationBackgroundService : BackgroundService
{
    private IOperationHandleService _operationHandleService;
    private IConnection _connection;
    private IModel _channel;

    public OperationBackgroundService(IOperationHandleService operationHandleService)
    {
        _operationHandleService = operationHandleService;
        InitRabbitMQ();
    }

    private void InitRabbitMQ()
    {
        var maxAttempts = 5;
        var attempt = 0;

        while (true)
        {
            try
            {
                var factory = new ConnectionFactory() { HostName = "localhost", UserName = "user", Password = "password", Port = 5672 };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();

                _channel.QueueDeclare(queue: "operationsQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
                break;
            }
            catch (BrokerUnreachableException ex)
            {
                attempt++;
                if (attempt >= maxAttempts)
                {
                    Console.WriteLine("Failed to connect to RabbitMQ after several attempts.");
                    break;
                }

                Console.WriteLine($"Connection attempt {attempt} failed. Will try again in a 4 seconds.");
                Task.Delay(TimeSpan.FromSeconds(4)).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while trying to connect to RabbitMQ: {ex.Message}");
                break;
            }
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();
        if (_channel != null)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (ch, ea) =>
            {
                var content = Encoding.UTF8.GetString(ea.Body.ToArray());

                var dto = JsonSerializer.Deserialize<CreateOperationDto>(content);

                try
                {
                    _operationHandleService.CreateOperation(dto);

                    _channel.BasicAck(ea.DeliveryTag, false);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error processing message");
                    _channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            _channel.BasicConsume(queue: "operationsQueue", autoAck: false, consumer: consumer);
        }
        else
        {
            Console.WriteLine("Connection to RabbitMQ failed, _channel is null.");
        }

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override void Dispose()
    {
        if (_channel != null && _connection != null)
        {
            _channel.Close();
            _connection.Close();
        }
        base.Dispose();
    }
}
