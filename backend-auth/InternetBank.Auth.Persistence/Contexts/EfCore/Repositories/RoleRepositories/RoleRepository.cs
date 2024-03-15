using InternetBank.Auth.Application.Interfaces.Repositories.RoleRepositories;
using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.RoleRepositories;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Role?> GetRoleByName(string name)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(e => e.Name == name);
    }
}
