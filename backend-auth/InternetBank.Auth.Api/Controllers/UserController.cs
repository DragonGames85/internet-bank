using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserGetService _userGetService;
    private readonly IUserHandleService _userHandleService;
    private IMonitoring _monitoring;

    public UserController(IUserGetService userGetService, IUserHandleService userHandleService, IMonitoring monitoring)
    {
        _userGetService = userGetService;
        _userHandleService = userHandleService;
        _monitoring = monitoring;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _userGetService.GetAllUsers();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/all", "GET", 200, 1, "Get all users");

            return Ok(result);
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost("{userId}/ban")]
    public async Task<ActionResult> ToggleBanUser(Guid userId)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _userHandleService.ToggleBanUser(userId);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/{userId}/ban", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/{userId}/ban", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _userHandleService.DeleteUser(id);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/{id}", "DELETE", 200, 1, "Delete user");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/User/{id}", "DELETE", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
