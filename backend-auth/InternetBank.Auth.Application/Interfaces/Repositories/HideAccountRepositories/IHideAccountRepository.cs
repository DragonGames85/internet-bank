using InternetBank.Auth.Domain.Entities;

namespace InternetBank.Auth.Application.Interfaces.Repositories.HideAccountRepositories;

public interface IHideAccountRepository
{
    Task<List<HideAccount>> GetHideAccountsByUserId(Guid userId);
    Task<HideAccount?> GetHideAccountByAccountId(Guid accountId);
}
