using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Queries;

public class GetUserAccountsQuery : IRequest<List<AccountDto>>
{
    public Guid Id { get; set; }
    public GetUserAccountsQuery(Guid id)
    {
        Id = id;
    }
}

public class GetUserAccountsQueryHandler : IRequestHandler<GetUserAccountsQuery, List<AccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserAccountsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AccountDto>> Handle(GetUserAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _unitOfWork.AccountRepository.GetAccountsIncludesCurrencyByUserId(request.Id);

        var dtoAccounts = new List<AccountDto>();

        foreach (var account in accounts)
        {
            var dtoCurrency = new CurrencyDto(account.AccountCurrency.Name, account.AccountCurrency.Symbol);
            var dtoUser = new UserDto(Guid.NewGuid(), "Benjamin Batton");

            dtoAccounts.Add(new AccountDto(
                account.Id,
                account.Number,
                account.Balance,
                account.Type,
                account.CreatedDate,
                account.ClosedDate,
                dtoUser,
                dtoCurrency));
        }

        return dtoAccounts;
    }
}
