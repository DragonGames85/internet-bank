using InternetBank.Auth.Application.Interfaces.Repositories.ConfigRepositories;
using InternetBank.Auth.Application.Interfaces.Repositories.HideAccountRepositories;
using InternetBank.Auth.Application.Interfaces.Repositories.RoleRepositories;
using InternetBank.Auth.Application.Interfaces.Repositories.UserRepositories;
using InternetBank.Auth.Domain.Common;

namespace InternetBank.Auth.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity;
    IUserRepository UserRepository { get; }
    IRoleRepository RoleRepository { get; }
    IConfigRepository ConfigRepository { get; }
    IHideAccountRepository HideAccountRepository { get; }
    Task<int> Save(CancellationToken cancellationToken);
    Task RollBack();
}
