using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Commands;

public class DeleteAccountCommand : IRequest
{
    public Guid Id { get; set; }
    public DeleteAccountCommand(Guid id)
    {
        Id = id;
    }
}

public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<Account>().GetById(request.Id)
            ?? throw new NullReferenceException("Account is not found.");

        await _unitOfWork.Repository<Account>().DeleteAsync(account);

        await _unitOfWork.Save(cancellationToken);
    }
}
