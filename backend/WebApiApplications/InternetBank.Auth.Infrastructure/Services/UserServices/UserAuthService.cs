using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.AccountServices;

public class UserAuthService : IUserAuthService
{
    private readonly IMediator _mediator;

    public UserAuthService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TokenDto> LoginUser(LoginUserDto dto)
    {
        var result = _mediator.Send(new LoginUserCommand(dto));

        return result;
    }
}
