using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace InternetBank.Core.Api.Controllers;

[ApiController]
[Route("core/api/[controller]")]
public class OperationController : ControllerBase
{
    private readonly IOperationGetService _operationGetService;
    private readonly IOperationHandleService _operationHandleService;

    public OperationController(IOperationGetService operationGetService, IOperationHandleService operationHandleService)
    {
        _operationGetService = operationGetService;
        _operationHandleService = operationHandleService;
    }

    [HttpGet("my")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<OperationDto>>> GetMyOperations()
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await _operationGetService.GetOperationsByUserId(Guid.Parse(userIdClaim.Value));

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<OperationDto>>> GetOperationsByUserId(Guid userId)
    {
        try
        {
            var result = await _operationGetService.GetOperationsByUserId(userId);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("account/{accountId}")]
    public async Task<ActionResult<List<OperationDto>>> GetOperationsByAccountId(Guid accountId)
    {
        try
        {
            var result = await _operationGetService.GetOperationsByAccountId(accountId);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<OperationDto>>> GetAllOperations()
    {
        try
        {
            var result = await _operationGetService.GetAllOperations();

            return Ok(result);
        }
        catch (Exception e)
        {
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
    public async Task<ActionResult> CreateOperation(CreateOperationDto dto)
    {
        try
        {
            await _operationHandleService.CreateOperation(dto);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
