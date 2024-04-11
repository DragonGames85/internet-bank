using InternetBank.Core.Domain.Entities;

namespace InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;

public interface IAccountRepository
{
    public Task<IEnumerable<Account>> GetAccountsIncludedCurrencyByUserId(Guid id);
    public Task<IEnumerable<Account>> GetAllAccountsIncludedCurrency();
    public Task<Account?> GetAccountByNumber(string number);
    public Task<Account?> GetAccountIncludedCurrencyById(Guid id);
    public Task<Account?> GetMasterAccountWithValue(decimal value);
}