using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Domain.Entities;

namespace InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;

public interface IJwtTokenService
{
    Task<TokenDto> GenerateToken(User user);
}
