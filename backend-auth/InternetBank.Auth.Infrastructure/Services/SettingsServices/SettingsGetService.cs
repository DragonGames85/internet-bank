using InternetBank.Auth.Application.DTOs.HideAccountDTOs;
using InternetBank.Auth.Application.Features.Settings.Queries;
using InternetBank.Auth.Application.Interfaces.Services.SettingsServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.SettingsServices;

public class SettingsGetService : ISettingsGetService
{
    private readonly IMediator _mediator;

    public SettingsGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<HideAccountDto>> GetHideAccounts(Guid userId)
    {
        var result = await _mediator.Send(new GetHideAccountsQuery(userId));

        return result;
    }
}
