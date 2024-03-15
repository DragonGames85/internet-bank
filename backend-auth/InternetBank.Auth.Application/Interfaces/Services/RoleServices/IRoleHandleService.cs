namespace InternetBank.Auth.Application.Interfaces.Services.RoleServices;

public interface IRoleHandleService
{
    Task CreateRole(string name);
    Task DeleteRoleByName(string name);
}
