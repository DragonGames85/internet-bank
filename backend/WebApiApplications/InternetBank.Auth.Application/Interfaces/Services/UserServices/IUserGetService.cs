using InternetBank.Auth.Application.DTOs.UserDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.UserServices;

public interface IUserGetService
{
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<UserDto> GetUserById(Guid Id);
}
