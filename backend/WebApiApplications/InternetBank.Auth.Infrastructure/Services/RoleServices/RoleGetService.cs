using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.Features.Roles.Queries;
using InternetBank.Auth.Application.Interfaces.Services.RoleServices;
using MediatR;

namespace InternetBank.Auth.Infrastructure.Services.RoleServices;

public class RoleGetService : IRoleGetService
{
    private readonly IMediator _mediator;

    public RoleGetService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IEnumerable<RoleDto>> GetAllRoles()
    {
        var result = await _mediator.Send(new GetAllRoleQuery());

        return result;
    }

    public async Task<RoleDto> GetRoleByName(string name)
    {
        var result = await _mediator.Send(new GetRoleByNameQuery(name))
            ?? throw new Exception("Role is not found.");

        return new RoleDto(result.Id, result.Name);
    }
}
