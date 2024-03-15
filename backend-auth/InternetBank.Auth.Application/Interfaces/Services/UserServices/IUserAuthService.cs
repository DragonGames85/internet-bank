using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.UserServices;

public interface IUserAuthService
{
    Task<TokenDto> LoginUser(LoginUserDto dto);
}
