using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.UserServices;

public interface IUserHandleService
{
    Task<TokenDto> CreateUser(CreateUserDto dto);
    Task DeleteUser(Guid id);
    Task ToggleBanUser(Guid id);
}
