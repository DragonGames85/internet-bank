using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.DTOs.HideAccountDTOs;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("auth/api/[controller]")]
public class SettingsController : ControllerBase
{
    private readonly ISettingsGetService _settingsGetService;
    private readonly ISettingsHandleService _settingsHandleService;

    public SettingsController(ISettingsGetService settingsGetService, ISettingsHandleService settingsHandleService)
    {
        _settingsGetService = settingsGetService;
        _settingsHandleService = settingsHandleService;
    }

    [HttpPost("config")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> ChangeConfig(ConfigDto dto)
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await _settingsHandleService.ChangeConfig(Guid.Parse(userIdClaim.Value), dto);

            return Ok();
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult<List<HideAccountDto>>> GetHideAccounts()
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            var result = await _settingsGetService.GetHideAccounts(Guid.Parse(userIdClaim.Value));

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> AddHideAccount(Guid accountId)
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await _settingsHandleService.AddHideAccount(Guid.Parse(userIdClaim.Value), accountId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("hideAccount")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> DeleteHideAccount(Guid accountId)
    {
        try
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId")
                ?? throw new Exception("userId is not found.");

            await _settingsHandleService.DeleteHideAccount(Guid.Parse(userIdClaim.Value), accountId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
