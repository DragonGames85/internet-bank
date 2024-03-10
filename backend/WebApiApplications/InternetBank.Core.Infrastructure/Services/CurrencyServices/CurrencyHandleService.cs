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

    public async Task EditCurrency(Guid id, ShortCurrencyDto dto)
    {
        await _mediator.Send(new EditCurrencyCommand(id, dto));
    }

    public async Task DeleteCurrency(Guid id)
    {
        await _mediator.Send(new DeleteCurrencyCommand(id));
    }
}
