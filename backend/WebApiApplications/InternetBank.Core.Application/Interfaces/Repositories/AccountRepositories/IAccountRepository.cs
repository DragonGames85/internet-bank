using InternetBank.Core.Domain.Entities;

namespace InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;

public interface IAccountRepository
{
    public Task<IEnumerable<Account>> GetAccountsIncludesCurrencyByUserId(Guid id);
    public Task<IEnumerable<Account>> GetAllAccountsIncludesCurrency();
}