using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Features.Accounts.Queries;
using InternetBank.Core.Application.Interfaces.Services.AccountServices;
using MediatR;

namespace InternetBank.Core.Infrastructure.Services.AccountServices;

public class AccountGetService : IAccountGetService
{
    private readonly IMediator _mediator;

    public AccountGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<AccountDto>> GetAccounts(Guid userId)
    {
        var result = await _mediator.Send(new GetUserAccountsQuery(userId));

        return result;
    }

    public async Task<List<AccountDto>> GetAllAccounts()
    {
        var result = await _mediator.Send(new GetAllAccountsQuery());

        return result;
    }

    public async Task<AccountDto> GetAccount(Guid id)
    {
        var result = await _mediator.Send(new GetAccountQuery(id));

        return result;
    }
}
