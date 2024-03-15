using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Features.Currencies.Queries;
using InternetBank.Auth.Application.Interfaces.Services.UserServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.UserServices;

public class UserGetService : IUserGetService
{
    private readonly IMediator _mediator;

    public UserGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());

        return users;
    }

    public async Task<UserDto> GetUserById(Guid id)
    {
        var user = await _mediator.Send(new GetUserQuery(id))
            ?? throw new Exception("User is not found.");

        return user;
    }
}
