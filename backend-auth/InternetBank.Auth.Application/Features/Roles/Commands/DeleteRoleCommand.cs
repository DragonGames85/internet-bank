using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Roles.Commands;

public class DeleteRoleCommand : IRequest
{
    public string Name { get; }

    public DeleteRoleCommand(string name)
    {
        Name = name;
    }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.RoleRepository.GetRoleByName(request.Name)
            ?? throw new NullReferenceException("Role is not found.");

        await _unitOfWork.Repository<Role>().DeleteAsync(role);

        await _unitOfWork.Save(cancellationToken);
    }
}
