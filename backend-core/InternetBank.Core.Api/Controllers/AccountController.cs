using InternetBank.Core.Api;
using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Diagnostics;
using System.Security.Claims;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("core/api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountGetService _accountGetService;
    private readonly IAccountHandleService _accountHandleService;
    private readonly IMonitoring _monitoring;
    public AccountController(IAccountGetService accountGetService, IAccountHandleService accountHandleService, IMonitoring monitoring)
    {
        _accountGetService = accountGetService;
        _accountHandleService = accountHandleService;
        _monitoring = monitoring;
    }

    [HttpGet("my")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<AccountDto>>> GetMyAccounts()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await _accountGetService.GetAccounts(Guid.Parse(userIdClaim.Value));

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/my", "GET", 200, 1, "");

            return Ok(result);
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/my", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<AccountDto>>> GetUserAccounts(Guid userId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _accountGetService.GetAccounts(userId);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/user/{userId}", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/user/{userId}", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccount(Guid id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _accountGetService.GetAccount(id);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<AccountDto>>> GetAllAccounts()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _accountGetService.GetAllAccounts();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/all", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount(CreateAccountDto dto, Guid? userId, int? value = 0)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _accountHandleService.CreateAccount(dto, userId ?? Guid.NewGuid(), value);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<AccountDto>>> EditAccount(Guid id, EditAccountDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _accountHandleService.EditAccount(id, dto);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "PUT", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "PUT", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<AccountDto>>> DeleteAccount(Guid id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _accountHandleService.DeleteAccount(id);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "DELETE", 200, 1, "");
            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Account/{id}", "DELETE", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
