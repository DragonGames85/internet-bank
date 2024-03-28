using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserAuthService _userAuthService;
    private readonly IUserHandleService _userHandleService;

    public AuthController(IUserAuthService userAuthService, IUserHandleService authHandleService)
    {
        _userAuthService = userAuthService;
        _userHandleService = authHandleService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<TokenDto>> CreateUser(CreateUserDto dto)
    {
        try
        {
            var result = await _userHandleService.CreateUser(dto);

            return Ok(result);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenDto>> LoginUser(LoginUserDto dto)
    {
        try
        {
            var result = await _userAuthService.LoginUser(dto);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
