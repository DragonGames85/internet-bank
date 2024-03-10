using InternetBank.Auth.Domain.Common;

namespace InternetBank.Auth.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
    Task<int> Save(CancellationToken cancellationToken);
    Task RollBack();
}
