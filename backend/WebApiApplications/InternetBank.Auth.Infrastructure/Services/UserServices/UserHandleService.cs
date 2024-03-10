using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Features.Currencies.Commands;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.AccountServices;

public class UserHandleService : IUserHandleService
{
    private readonly IMediator _mediator;
    private readonly IJwtService _jwtService;

    public UserHandleService(IMediator mediator, IJwtService jwtService)
    {
        _mediator = mediator;
        _jwtService = jwtService
    }

    public async Task<TokenDto> CreateUser(CreateUserDto dto)
    {
        await _mediator.Send(new CreateUserCommand(dto));

        await _mediator.Send(new GetJwtSecurityTokenCommand())

        return result;
    }

    public Task BanToggleUser(Guid id)
    {
        var result = _mediator.Send(new BanToggleUserCommand(id));

        return result;
    }
}
