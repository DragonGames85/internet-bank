using InternetBank.Auth.Api;
using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleGetService _roleGetService;
    private readonly IRoleHandleService _roleHandleService;
    private IMonitoring _monitoring;
    public RoleController(IRoleGetService roleGetService, IRoleHandleService roleHandleService, IMonitoring monitoring)
    {
        _roleGetService = roleGetService;
        _roleHandleService = roleHandleService;
        _monitoring = monitoring;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _roleGetService.GetAllRoles();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role/all", "GET", 200, 1, "");

            return Ok(result);
        } 
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role/all", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateRole(string name)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _roleHandleService.CreateRole(name);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteRole(string name)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _roleHandleService.DeleteRoleByName(name);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role/{name}", "DELETE", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "auth/api/Role/{name}", "DELETE", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
