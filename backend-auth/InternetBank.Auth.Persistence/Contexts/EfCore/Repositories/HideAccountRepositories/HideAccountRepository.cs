using InternetBank.Auth.Application.Interfaces.Repositories.HideAccountRepositories;
using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.HideAccountRepositories;

public class HideAccountRepository : IHideAccountRepository
{
    private readonly ApplicationDbContext _context;

    public HideAccountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<HideAccount?> GetHideAccountByAccountId(Guid accountId)
    {
        return await _context.HideAccounts
            .FirstOrDefaultAsync(e => e.AccountId == accountId);
    }

    public async Task<List<HideAccount>> GetHideAccountsByUserId(Guid userId)
    {
        return await _context.HideAccounts
            .Include(e => e.HideAccountUser)
            .Where(e => e.HideAccountUser.Id == userId)
            .ToListAsync();
    }
}
