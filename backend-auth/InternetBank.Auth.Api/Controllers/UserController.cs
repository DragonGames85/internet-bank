using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserGetService _userGetService;
    private readonly IUserHandleService _userHandleService;

    public UserController(IUserGetService userGetService, IUserHandleService userHandleService)
    {
        _userGetService = userGetService;
        _userHandleService = userHandleService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers()
    {
        try
        {
            var result = await _userGetService.GetAllUsers();

            return Ok(result);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("ban/{userId}")]
    public async Task<ActionResult> ToggleBanUser(Guid userId)
    {
        try
        {
            await _userHandleService.ToggleBanUser(userId);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id)
    {
        try
        {
            await _userHandleService.DeleteUser(id);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
