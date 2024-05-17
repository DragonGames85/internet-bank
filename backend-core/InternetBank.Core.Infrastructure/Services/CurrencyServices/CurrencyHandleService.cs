using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Features.Currencies.Commands;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.CurrencyServices;

public class CurrencyHandleService : ICurrencyHandleService
{
    private readonly IMediator _mediator;

    public CurrencyHandleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateCurrency(ShortCurrencyDto dto)
    {
        await _mediator.Send(new CreateCurrencyCommand(dto));
    }
    public async Task CreateAllCurrency()
    {
        await CreateCurrency(new ShortCurrencyDto("RUB", "₽"));
        await CreateCurrency(new ShortCurrencyDto("AUD", "$"));
        await CreateCurrency(new ShortCurrencyDto("AZN", "₼"));
        await CreateCurrency(new ShortCurrencyDto("GBP", "£"));
        await CreateCurrency(new ShortCurrencyDto("AMD", "֏"));
        await CreateCurrency(new ShortCurrencyDto("BYN", "Br"));
        await CreateCurrency(new ShortCurrencyDto("BGN", "лв"));
        await CreateCurrency(new ShortCurrencyDto("BRL", "R$"));
        await CreateCurrency(new ShortCurrencyDto("HUF", "Ft"));
        await CreateCurrency(new ShortCurrencyDto("VND", "₫"));
        await CreateCurrency(new ShortCurrencyDto("HKD", "HK$"));
        await CreateCurrency(new ShortCurrencyDto("GEL", "₾"));
        await CreateCurrency(new ShortCurrencyDto("DKK", "kr"));
        await CreateCurrency(new ShortCurrencyDto("AED", "د.إ"));
        await CreateCurrency(new ShortCurrencyDto("USD", "$"));
        await CreateCurrency(new ShortCurrencyDto("EUR", "€"));
        await CreateCurrency(new ShortCurrencyDto("EGP", "£"));
        await CreateCurrency(new ShortCurrencyDto("INR", "₹"));
        await CreateCurrency(new ShortCurrencyDto("IDR", "Rp"));
        await CreateCurrency(new ShortCurrencyDto("KZT", "₸"));
        await CreateCurrency(new ShortCurrencyDto("CAD", "$"));
        await CreateCurrency(new ShortCurrencyDto("QAR", "﷼"));
        await CreateCurrency(new ShortCurrencyDto("KGS", "лв"));
        await CreateCurrency(new ShortCurrencyDto("CNY", "¥"));
        await CreateCurrency(new ShortCurrencyDto("MDL", "L"));
        await CreateCurrency(new ShortCurrencyDto("NZD", "$"));
        await CreateCurrency(new ShortCurrencyDto("NOK", "kr"));
        await CreateCurrency(new ShortCurrencyDto("PLN", "zł"));
        await CreateCurrency(new ShortCurrencyDto("RON", "lei"));
        await CreateCurrency(new ShortCurrencyDto("XDR", "SDR"));
        await CreateCurrency(new ShortCurrencyDto("SGD", "$"));
        await CreateCurrency(new ShortCurrencyDto("TJS", "ЅМ"));
        await CreateCurrency(new ShortCurrencyDto("THB", "฿"));
        await CreateCurrency(new ShortCurrencyDto("TRY", "₺"));
        await CreateCurrency(new ShortCurrencyDto("TMT", "m"));
        await CreateCurrency(new ShortCurrencyDto("UZS", "сўм"));
        await CreateCurrency(new ShortCurrencyDto("UAH", "₴"));
        await CreateCurrency(new ShortCurrencyDto("CZK", "Kč"));
        await CreateCurrency(new ShortCurrencyDto("SEK", "kr"));
        await CreateCurrency(new ShortCurrencyDto("CHF", "CHF"));
        await CreateCurrency(new ShortCurrencyDto("RSD", "дин"));
        await CreateCurrency(new ShortCurrencyDto("ZAR", "R"));
        await CreateCurrency(new ShortCurrencyDto("KRW", "₩"));
        await CreateCurrency(new ShortCurrencyDto("JPY", "¥"));
    }

    public async Task EditCurrency(Guid id, ShortCurrencyDto dto)
    {
        await _mediator.Send(new EditCurrencyCommand(id, dto));
    }

    public async Task DeleteCurrency(Guid id)
    {
        await _mediator.Send(new DeleteCurrencyCommand(id));
    }
}
