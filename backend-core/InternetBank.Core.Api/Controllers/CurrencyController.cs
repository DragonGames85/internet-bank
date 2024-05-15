using InternetBank.Core.Api;
using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using InternetBank.Core.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WebApiCoreApplication.Controllers;

[ApiController]
[Route("core/api/[controller]")]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyGetService _currencyGetService;
    private readonly ICurrencyHandleService _currencyHandleService;
    private readonly IMonitoring _monitoring;

    public CurrencyController(ICurrencyGetService currencyGetService, ICurrencyHandleService currencyHandleService, IMonitoring monitoring)
    {
        _currencyGetService = currencyGetService;
        _currencyHandleService = currencyHandleService;
        _monitoring = monitoring;
    }

    [HttpGet]
    public async Task<ActionResult<List<CurrencyDto>>> GetCurrencies()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            var result = await _currencyGetService.GetCurrencies();

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency", "GET", 200, 1, "");

            return Ok(result);
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency", "GET", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateCurrency(ShortCurrencyDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _currencyHandleService.CreateCurrency(dto);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> EditCurrency(Guid id, ShortCurrencyDto dto)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _currencyHandleService.EditCurrency(id, dto);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/{id}", "PUT", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/{id}", "PUT", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCurrency(Guid id)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _currencyHandleService.DeleteCurrency(id);

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/{id}", "DELETE", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/{id}", "DELETE", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }

    [HttpPost("all")]
    public async Task<ActionResult> CreateAllCurrency()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        try
        {
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("RUB", "₽"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("AUD", "$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("AZN", "₼"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("GBP", "£"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("AMD", "֏"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("BYN", "Br"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("BGN", "лв"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("BRL", "R$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("HUF", "Ft"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("VND", "₫"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("HKD", "HK$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("GEL", "₾"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("DKK", "kr"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("AED", "د.إ"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("USD", "$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("EUR", "€"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("EGP", "£"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("INR", "₹"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("IDR", "Rp"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("KZT", "₸"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("CAD", "$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("QAR", "﷼"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("KGS", "лв"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("CNY", "¥"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("MDL", "L"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("NZD", "$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("NOK", "kr"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("PLN", "zł"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("RON", "lei"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("XDR", "SDR"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("SGD", "$"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("TJS", "ЅМ"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("THB", "฿"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("TRY", "₺"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("TMT", "m"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("UZS", "сўм"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("UAH", "₴"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("CZK", "Kč"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("SEK", "kr"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("CHF", "CHF"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("RSD", "дин"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("ZAR", "R"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("KRW", "₩"));
            await _currencyHandleService.CreateCurrency(new ShortCurrencyDto("JPY", "¥"));

            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/all", "POST", 200, 1, "");

            return Ok();
        }
        catch (Exception e)
        {
            stopwatch.Stop();
            TimeSpan executionTime = stopwatch.Elapsed;
            _monitoring.MonitoringService(executionTime, "core/api/Currency/all", "POST", 400, 0, e.Message);

            return BadRequest(e.Message);
        }
    }
}
