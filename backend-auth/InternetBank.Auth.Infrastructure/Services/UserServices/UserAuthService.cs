using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Features.Users.Queries;
using InternetBank.Auth.Application.Interfaces.Services.JwtTokenServices;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.UserServices;

public class UserAuthService : IUserAuthService
{
    private readonly IMediator _mediator;
    private readonly IJwtTokenService _jwtTokenService;

    public UserAuthService(IMediator mediator, IJwtTokenService jwtTokenService)
    {
        _mediator = mediator;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<TokenDto> LoginUser(LoginUserDto dto)
    {
        var user = await _mediator.Send(new GetUserByCredentialsQuery(dto)) 
            ?? throw new Exception("Login or password isn't correct.");

        var token = await _jwtTokenService.GenerateToken(user);

        return token;
    }
}
