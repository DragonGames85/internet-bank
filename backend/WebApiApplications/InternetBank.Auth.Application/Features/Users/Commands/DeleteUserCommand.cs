using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Users.Commands;

public class DeleteUserCommand : IRequest
{
    public Guid Id { get; set; }
    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetById(request.Id)
            ?? throw new NullReferenceException("User is not found.");

        await _unitOfWork.Repository<User>().DeleteAsync(user);

        await _unitOfWork.Save(cancellationToken);
    }
}
