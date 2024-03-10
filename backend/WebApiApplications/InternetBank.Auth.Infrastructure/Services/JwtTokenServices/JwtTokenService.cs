using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;
using InternetBank.Auth.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InternetBank.Auth.Infrastructure.Services.JwtServices;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<TokenDto> GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Name),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            // Добавьте дополнительные утверждения/клеймы если необходимо
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10080),
            signingCredentials: credentials
        );

        var tokenDto = new TokenDto(
            new JwtSecurityTokenHandler().WriteToken(token),
            user.Id);

        return Task.FromResult(tokenDto);
    }
}
