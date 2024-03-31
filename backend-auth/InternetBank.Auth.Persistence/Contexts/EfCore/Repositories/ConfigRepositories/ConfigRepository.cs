using InternetBank.Auth.Application.Interfaces.Repositories.ConfigRepositories;
using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.ConfigRepositories;

public class ConfigRepository : IConfigRepository
{
    private readonly ApplicationDbContext _context;

    public ConfigRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Config?> GetByUserId(Guid userId)
    {
        return await _context.Configs
            .Include(e => e.User)
            .FirstOrDefaultAsync(e => e.User.Id == userId);
    }
}
