using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Features.Accounts.Commands;
using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.AccountServices;

public class AccountHandleService : IAccountHandleService
{
    private readonly IMediator _mediator;

    public AccountHandleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateAccount(CreateAccountDto dto, Guid userId)
    {
        await _mediator.Send(new CreateAccountCommand(dto, userId));
    }

    public async Task CloseAccount(Guid id)
    {
        await _mediator.Send(new CloseAccountCommand(id));
    }

    public async Task EditAccount(Guid id, EditAccountDto dto)
    {
        await _mediator.Send(new EditAccountCommand(id, dto));
    }

    public async Task DeleteAccount(Guid id)
    {
        await _mediator.Send(new DeleteAccountCommand(id));
    }
}
