using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using Microsoft.AspNetCore.Mvc;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyGetService _currencyGetService;
    private readonly ICurrencyHandleService _currencyHandleService;

    public CurrencyController(ICurrencyGetService currencyGetService, ICurrencyHandleService currencyHandleService)
    {
        _currencyGetService = currencyGetService;
        _currencyHandleService = currencyHandleService;
    }

    [HttpGet]
    public async Task<ActionResult<List<CurrencyDto>>> GetCurrencies()
    {
        try
        {
            var result = await _currencyGetService.GetCurrencies();

            return Ok(result);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateCurrency(ShortCurrencyDto dto)
    {
        try
        {
            await _currencyHandleService.CreateCurrency(dto);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditCurrency(Guid id, ShortCurrencyDto dto)
    {
        try
        {
            await _currencyHandleService.EditCurrency(id, dto);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCurrency(Guid id)
    {
        try
        {
            await _currencyHandleService.DeleteCurrency(id);

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
