using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Queries;

public class GetAllAccountsQuery : IRequest<List<AccountDto>>
{
}

public class GetAllAccountsQueryHandler : IRequestHandler<GetAllAccountsQuery, List<AccountDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllAccountsQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<AccountDto>> Handle(GetAllAccountsQuery request, CancellationToken cancellationToken)
    {
        var accounts = await _unitOfWork.AccountRepository.GetAllAccountsIncludesCurrency();

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
