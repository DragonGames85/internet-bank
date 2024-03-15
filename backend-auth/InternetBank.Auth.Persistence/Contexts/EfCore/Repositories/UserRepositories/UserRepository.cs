using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories.UserRepositories;
using InternetBank.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Persistence.Contexts.EfCore.Repositories.UserRepositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllUsersIncludedRoles()
    {
        return await _context.Users
            .Include(e => e.UserRoles)
            .ToListAsync();
    }

    public async Task<User?> GetUserByLoginPasswordIncludedRoles(LoginUserDto dto)
    {
        return await _context.Users
            .Include(e => e.UserRoles)
            .FirstOrDefaultAsync(e => e.Login == dto.Login && e.Password == dto.Password);
    }
}
