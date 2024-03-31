using InternetBank.Auth.Application.DTOs.ConfigDTOs;
using InternetBank.Auth.Application.Features.Settings.Commands;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.SettingsServices;

public class SettingsHandleService : ISettingsHandleService
{
    private readonly IMediator _mediator;

    public SettingsHandleService(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task ChangeConfig(Guid userId, ConfigDto dto)
    {
        await _mediator.Send(new ChangeConfigCommand(userId, dto));
    }

    public async Task AddHideAccount(Guid userId, Guid accountId)
    {
        await _mediator.Send(new AddHideAccountCommand(userId, accountId));
    }

    public async Task DeleteHideAccount(Guid userId, Guid accountId)
    {
        await _mediator.Send(new DeleteHideAccountCommand(userId, accountId));
    }
}
