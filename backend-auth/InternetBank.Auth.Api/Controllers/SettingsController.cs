using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.DTOs.HideAccountDTOs;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsGetService _settingsGetService;
    private readonly ISettingsHandleService _settingsHandleService;
    private IMonitoring _monitoring;

    public SettingsController(ISettingsGetService settingsGetService, ISettingsHandleService settingsHandleService, IMonitoring monitoring)
    {
        _settingsGetService = settingsGetService;
        _settingsHandleService = settingsHandleService;
        _monitoring = monitoring;
    }

    [HttpPost("config")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> ChangeConfig(ConfigDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await Retry.Do(() => _settingsHandleService.ChangeConfig(Guid.Parse(userIdClaim.Value), dto), TimeSpan.FromSeconds(1));
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/config", "POST", 200, 1, "");

            return Ok();
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/config", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<HideAccountDto>>> GetHideAccounts()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await Retry.Do(() => _settingsGetService.GetHideAccounts(Guid.Parse(userIdClaim.Value)), TimeSpan.FromSeconds(1));
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> AddHideAccount(Guid accountId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await Retry.Do(() => _settingsHandleService.AddHideAccount(Guid.Parse(userIdClaim.Value), accountId), TimeSpan.FromSeconds(1));

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> DeleteHideAccount(Guid accountId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

             await Retry.Do(() => _settingsHandleService.DeleteHideAccount(Guid.Parse(userIdClaim.Value), accountId), TimeSpan.FromSeconds(1));
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "DELETE", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Settings/hideAccount", "DELETE", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
