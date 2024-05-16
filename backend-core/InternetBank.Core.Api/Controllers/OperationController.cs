using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using InternetBank.Core.Infrastructure.Hubs.OperationHubs;
using Microsoft.AspNet.SignalR.Messaging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Text;

namespace InternetBank.Core.Api.Controllers;

[ApiController]
[Route("core/api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly IOperationGetService _operationGetService;
    private readonly IOperationHandleService _operationHandleService;
    private readonly IHubContext<OperationHub> _hubContext;
    private readonly IMonitoring _monitoring;

    public OperationController(IOperationGetService operationGetService, IOperationHandleService operationHandleService, IHubContext<OperationHub> hubContext, IMonitoring monitoring)
    {
        _operationGetService = operationGetService;
        _operationHandleService = operationHandleService;
        _hubContext = hubContext;
        _monitoring = monitoring;
    }

    [HttpGet("my")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<OperationDto>>> GetMyOperations()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ??  throw new Exception("userId is not found."); 

            var result = await _operationGetService.GetOperationsByUserId(Guid.Parse(userIdClaim.Value));

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/my", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/my", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<OperationDto>>> GetOperationsByUserId(Guid userId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            var result = await _operationGetService.GetOperationsByUserId(userId);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/user/{userId}", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/user/{userId}", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<List<OperationDto>>> GetOperationsByAccountId(Guid accountId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            var result = await _operationGetService.GetOperationsByAccountId(accountId);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/account/{accountId}", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/account/{accountId}", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<OperationDto>>> GetAllOperations()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            var result = await _operationGetService.GetAllOperations();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/all", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }


    /*
    public Task<ActionResult> CreateOperationWithMQ(CreateOperationDto dto)
    {
        try
        {
            var factory = new ConnectionFactory() { HostName = "localhost", UserName = "user", Password = "password" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "operationsQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var message = JsonConvert.SerializeObject(dto);
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "operationsQueue",
                                     basicProperties: null,
                                     body: body);
            }

            return Task.FromResult<ActionResult>(Ok());
        }
        catch (Exception e)
        {
            return Task.FromResult<ActionResult>(BadRequest(e.Message));
        }
    }*/


    [HttpPost]
    public async Task<ActionResult> CreateOperation(CreateOperationDto dto, bool isCreditOperation = false)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            await _operationHandleService.CreateOperation(dto, isCreditOperation);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost("sendMessage")]
    public async Task<ActionResult> SendMessage()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _hubContext.Clients.All.SendAsync("ReceiveOperationsUpdate", "Sheesh");
            
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/sendMessage", "POST", 200, 1, "");

            return Ok();
        }
        catch(Exception e)
        {

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Operation/sendMessage", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
