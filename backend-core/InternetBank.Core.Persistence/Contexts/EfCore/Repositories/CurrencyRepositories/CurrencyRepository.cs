using InternetBank.Core.Application.Interfaces.Repositories.CurrencyRepositories;
using InternetBank.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Core.Persistence.Contexts.EfCore.Repositories.CurrencyRepositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly ApplicationDbContext _context;

    public CurrencyRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Currency?> GetCurrencyByName(string name)
    {
        return await _context.Currencies
            .FirstOrDefaultAsync(e => e.Name == name);
    }
}
