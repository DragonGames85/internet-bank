using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Roles.Queries;

public class GetRoleByNameQuery : IRequest<Role?>
{
    public string Name { get; }

    public GetRoleByNameQuery(string name)
    {
        Name = name;
    }
}

public class GetRoleByNameQueryHandler : IRequestHandler<GetRoleByNameQuery, Role?>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetRoleByNameQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Role?> Handle(GetRoleByNameQuery request, CancellationToken cancellationToken)
    {
        return await _unitOfWork.RoleRepository.GetRoleByName(request.Name);
    }
}
