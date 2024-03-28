using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<List<OperationDto>>> GetMyOperations()
    {
        try
        {
            var userIdClaim = User.FindFirst("id")
                ?? throw new Exception("Id is not found.");

            var result = await _operationGetService.GetOperationsByUserId(Guid.NewGuid());

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
            var result = await _operationGetService.GetOperationsByUserId(Guid.NewGuid());

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
            var result = await _operationGetService.GetOperationsByAccountId(Guid.NewGuid());

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

    [HttpPost]
    public async Task<ActionResult> CreateOperation(CreateOperationDto dto, Guid? userId)
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
