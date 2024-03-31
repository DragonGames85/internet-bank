using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;
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

    public Task<TokenDto> GenerateToken(UserWithConfigDto user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        Console.WriteLine(_configuration["Jwt:Key"]);

        var claims = new[]
        {
            new Claim("id", user.Id.ToString()),
            new Claim("userId", user.Id.ToString()),
            new Claim("name", user.Name),
            new Claim("role", user.Role),
            new Claim("isBanned", user.IsBanned.ToString()),
            new Claim("isLightTheme", user?.Config?.IsLightTheme != null ? user.Config.IsLightTheme.ToString() : "True"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            notBefore: DateTime.UtcNow,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10080),
            signingCredentials: credentials
        );

        var tokenDto = new TokenDto(
            new JwtSecurityTokenHandler().WriteToken(token),
            user.Id);

        return Task.FromResult(tokenDto);
    }
}
