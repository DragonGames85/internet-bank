using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Users.Commands;

public class ToggleBanUserCommand : IRequest
{
    public Guid Id { get; set; }
    public ToggleBanUserCommand(Guid id)
    {
        Id = id;
    }
}

public class ToggleBanUserCommandHandler : IRequestHandler<ToggleBanUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public ToggleBanUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(ToggleBanUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetById(request.Id);

        user.IsBanned = !user.IsBanned;

        await _unitOfWork.Repository<User>().UpdateAsync(user);

        await _unitOfWork.Save(cancellationToken);
    }
}
