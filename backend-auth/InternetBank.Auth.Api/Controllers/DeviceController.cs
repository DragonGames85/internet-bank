using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.DTOs.DeviceDTOs;
using InternetBank.Auth.Application.DTOs.HideAccountDTOs;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using InternetBank.Auth.Infrastructure.Services.DeviceServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;
    private IMonitoring _monitoring;

    public DeviceController(IDeviceService deviceService, IMonitoring monitoring)
    {
        _deviceService = deviceService;
        _monitoring = monitoring;
    }

    [HttpPost("create")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> AddDevice(string token)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await _deviceService.CreateDevice(Guid.Parse(userIdClaim.Value), token);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/create", "POST", 200, 1, "");

            return Ok();
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/create", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<DeviceDto>>> GetUserDevices(Guid userId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await _deviceService.GetUserDevices(Guid.Parse(userIdClaim.Value));

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/all", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpGet("employees")]
    public async Task<ActionResult<List<DeviceDto>>> GetEmployeesDevices()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await _deviceService.GetEmployeesDevices();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/all", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Device/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
