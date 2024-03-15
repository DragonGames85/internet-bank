using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Application.Interfaces.Repositories.RoleRepositories;
using InternetBank.Auth.Application.Interfaces.Repositories.UserRepositories;
using InternetBank.Auth.Domain.Common;
using InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.RoleRepositories;
using InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.UserRepositories;
using System.Collections;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IUserRepository UserRepository { get; }
    public IRoleRepository RoleRepository { get; }
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        UserRepository = new UserRepository(dbContext);
        RoleRepository = new RoleRepository(dbContext);
    }

    public IGenericRepository<T> Repository<T>() where T : BaseAuditableEntity
    {
        _repositories ??= new Hashtable();

        var type = typeof(T).Name;

        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(GenericRepository<>);

            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _dbContext);

            _repositories.Add(type, repositoryInstance);
        }

        return (IGenericRepository<T>)_repositories[type];
    }

    public Task RollBack()
    {
        _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        return Task.CompletedTask;
    }

    public async Task<int> Save(CancellationToken cancellationToken)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
