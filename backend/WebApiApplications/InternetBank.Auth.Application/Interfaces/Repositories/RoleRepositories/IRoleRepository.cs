using InternetBank.Auth.Domain.Entities;

namespace InternetBank.Auth.Application.Interfaces.Repositories.RoleRepositories;

public interface IRoleRepository
{
    Task<Role?> GetRoleByName(string name);
}
