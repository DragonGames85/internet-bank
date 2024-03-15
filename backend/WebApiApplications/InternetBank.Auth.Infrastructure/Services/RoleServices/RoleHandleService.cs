using InternetBank.Auth.Application.Features.Roles.Commands;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.RoleServices;

public class RoleHandleService : IRoleHandleService
{
    private readonly IMediator _mediator;

    public RoleHandleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task CreateRole(string name)
    {
        await _mediator.Send(new CreateRoleCommand(name));
    }

    public async Task DeleteRoleByName(string name)
    {
        await _mediator.Send(new DeleteRoleCommand(name));
    }
}
