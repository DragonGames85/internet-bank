using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Domain.Entities;

namespace InternetBank.Auth.Application.Interfaces.Repositories.UserRepositories;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersIncludedRoles();
    Task<User?> GetUserByLoginPasswordIncludedRoles(LoginUserDto dto);
}
