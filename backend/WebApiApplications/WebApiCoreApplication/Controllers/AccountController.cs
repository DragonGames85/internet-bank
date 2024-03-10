using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountGetService _accountGetService;
    private readonly IAccountHandleService _accountHandleService;

    public AccountController(IAccountGetService accountGetService, IAccountHandleService accountHandleService)
    {
        _accountGetService = accountGetService;
        _accountHandleService = accountHandleService;
    }

    [HttpGet("my")]
    public async Task<ActionResult<List<AccountDto>>> GetMyAccounts()
    {
        try
        {
            var result = await _accountGetService.GetAccounts(Guid.NewGuid());

            return Ok(result);
        } 
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<AccountDto>>> GetUserAccounts(Guid userId)
    {
        try
        {
            var result = await _accountGetService.GetAccounts(userId);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AccountDto>> GetAccount(Guid id)
    {
        try
        {
            var result = await _accountGetService.GetAccount(id);

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<AccountDto>>> GetAllAccounts()
    {
        try
        {
            var result = await _accountGetService.GetAllAccounts();

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateAccount(CreateAccountDto dto, Guid? userId)
    {
        try
        {
            await _accountHandleService.CreateAccount(dto, userId ?? Guid.NewGuid());

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<List<AccountDto>>> EditAccount(Guid id, EditAccountDto dto)
    {
        try
        {
            await _accountHandleService.EditAccount(id, dto);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<List<AccountDto>>> DeleteAccount(Guid id)
    {
        try
        {
            await _accountHandleService.DeleteAccount(id);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
