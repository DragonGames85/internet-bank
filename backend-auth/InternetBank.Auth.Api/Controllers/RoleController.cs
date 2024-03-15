using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IRoleGetService _roleGetService;
    private readonly IRoleHandleService _roleHandleService;

    public RoleController(IRoleGetService roleGetService, IRoleHandleService roleHandleService)
    {
        _roleGetService = roleGetService;
        _roleHandleService = roleHandleService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<RoleDto>>> GetAllRoles()
    {
        try
        {
            var result = await _roleGetService.GetAllRoles();

            return Ok(result);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateRole(string name)
    {
        try
        {
            await _roleHandleService.CreateRole(name);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteRole(string name)
    {
        try
        {
            await _roleHandleService.DeleteRoleByName(name);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
