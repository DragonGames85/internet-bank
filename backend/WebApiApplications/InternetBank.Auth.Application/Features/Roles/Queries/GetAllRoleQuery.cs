using InternetBank.Auth.Application.DTOs.RoleDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Roles.Queries;

public class GetAllRoleQuery : IRequest<List<RoleDto>>
{
}

public class GetAllRoleQueryHandler : IRequestHandler<GetAllRoleQuery, List<RoleDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllRoleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<RoleDto>> Handle(GetAllRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = await _unitOfWork.Repository<Role>().GetAll();

        var dtoRoles = new List<RoleDto>();

        foreach (var role in roles)
        {
            dtoRoles.Add(new RoleDto(role.Id, role.Name));
        }

        return dtoRoles;
    }
}
