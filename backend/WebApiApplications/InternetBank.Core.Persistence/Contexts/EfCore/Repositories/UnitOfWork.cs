using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;
using InternetBank.Core.Application.Interfaces.Repositories.CurrencyRepositories;
using InternetBank.Core.Domain.Common;
using InternetBank.Core.Persistence.Contexts.EfCore.Repositories.AccountRepositories;
using InternetBank.Core.Persistence.Contexts.EfCore.Repositories.CurrencyRepositories;
using System.Collections;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    public IAccountRepository AccountRepository { get; }
    public ICurrencyRepository CurrencyRepository { get; }
    private Hashtable _repositories;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = new AccountRepository(dbContext);
        CurrencyRepository = new CurrencyRepository(dbContext);
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
