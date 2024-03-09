using InternetBank.Core.Application.Interfaces.Repositories.AccountRepositories;
using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Repositories.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    private readonly ApplicationDbContext _context;

    public AccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Account>> GetAccountsIncludesCurrencyByUserId(Guid id)
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .Where(e => e.CreatedBy == id)
            .ToListAsync();
    }

    public async Task<IEnumerable<Account>> GetAllAccountsIncludesCurrency()
    {
        return await _context.Accounts
            .Include(e => e.AccountCurrency)
            .ToListAsync();
    }
}
