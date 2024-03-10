using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Features.Currencies.Queries;
using InternetBank.Core.Application.Interfaces.Services.CurrencyServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.CurrencyServices;

public class CurrencyGetService : ICurrencyGetService
{
    private readonly IMediator _mediator;

    public CurrencyGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<CurrencyDto>> GetCurrencies()
    {
        var result = await _mediator.Send(new GetAllCurrenciesQuery());

        return result;
    }
}
