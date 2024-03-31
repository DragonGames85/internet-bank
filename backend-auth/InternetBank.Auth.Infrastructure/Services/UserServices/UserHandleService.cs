using InternetBank.Auth.Application.DTOs.TokenDTOs;
using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Features.Currencies.Queries;
using InternetBank.Auth.Application.Features.Settings.Commands;
using InternetBank.Auth.Application.Features.Users.Commands;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.UserServices;

public class UserHandleService : IUserHandleService
{
    private readonly IMediator _mediator;
    private readonly IUserAuthService _userAuthService;

    public UserHandleService(IMediator mediator, IUserAuthService userAuthService)
    {
        _mediator = mediator;
        _userAuthService = userAuthService;
    }

    public async Task<TokenDto> CreateUser(CreateUserDto dto)
    {
        await _mediator.Send(new CreateUserCommand(dto));

        var dtoToken = await _userAuthService.LoginUser(new LoginUserDto(dto.Login, dto.Password));

        await _mediator.Send(new CreateConfigCommand(dtoToken.UserId));

        return dtoToken;
    }

    public Task ToggleBanUser(Guid id)
    {
        var result = _mediator.Send(new ToggleBanUserCommand(id));

        return result;
    }

    public Task DeleteUser(Guid id)
    {
        var result = _mediator.Send(new DeleteUserCommand(id));

        return result;
    }
}
