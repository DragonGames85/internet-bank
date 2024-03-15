using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;

namespace InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;

public interface IJwtTokenService
{
    Task<TokenDto> GenerateToken(UserDto user);
}
