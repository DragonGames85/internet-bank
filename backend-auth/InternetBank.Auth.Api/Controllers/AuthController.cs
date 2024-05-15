using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IUserHandleService _userHandleService;
    private IMonitoring _monitoring;

    public AuthController(IUserAuthService userAuthService, IUserHandleService authHandleService, IMonitoring monitoring)
    {
        _userAuthService = userAuthService;
        _userHandleService = authHandleService;
        _monitoring = monitoring;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenDto>> CreateUser(CreateUserDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _userHandleService.CreateUser(dto);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Auth/register", "POST", 200, 1, "Successful registration");

            return Ok(result);
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Auth/register", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> LoginUser(LoginUserDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Auth/login", "POST", 200, 1, "Successful authorization");

            var result = await _userAuthService.LoginUser(dto);

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Auth/login", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
