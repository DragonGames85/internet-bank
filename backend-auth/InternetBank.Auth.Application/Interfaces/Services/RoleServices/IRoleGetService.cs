using InternetBank.Auth.Application.DTOs.RoleDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.RoleServices;

public interface IRoleGetService
{
    Task<IEnumerable<RoleDto>> GetAllRoles();
    Task<RoleDto> GetRoleByName(string name);
}
