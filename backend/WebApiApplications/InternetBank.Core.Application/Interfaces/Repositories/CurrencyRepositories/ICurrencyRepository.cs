using InternetBank.Core.Domain.Entities;

namespace InternetBank.Core.Application.Interfaces.Repositories.CurrencyRepositories;

public interface ICurrencyRepository
{
    public Task<Currency?> GetCurrencyByName(string name);
}
