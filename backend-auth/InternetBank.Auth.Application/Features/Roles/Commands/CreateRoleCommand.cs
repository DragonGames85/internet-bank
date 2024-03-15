using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Roles.Commands;

public class CreateRoleCommand : IRequest
{
    public string Name { get; }

    public CreateRoleCommand(string name)
    {
        Name = name;
    }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await _unitOfWork.Repository<Role>().AddAsync(Role.Create(request.Name));

        await _unitOfWork.Save(cancellationToken);
    }
}
