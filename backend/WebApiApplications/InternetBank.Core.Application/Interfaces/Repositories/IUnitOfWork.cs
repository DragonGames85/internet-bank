using InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;
using InternetBank.Core.Application.Interfaces.Repositories.CurrencyRepositories;
using InternetBank.Core.Domain.Common;

namespace InternetBank.Core.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    ICurrencyRepository CurrencyRepository { get; }
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
    Task<int> Save(CancellationToken cancellationToken);
    Task RollBack();
}
