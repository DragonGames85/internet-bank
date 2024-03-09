using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Commands;

public class CloseAccountCommand : IRequest
{
    public Guid Id { get; set; }
    public CloseAccountCommand(Guid id)
    {
        Id = id;
    }
}

public class CloseAccountCommandHandler : IRequestHandler<CloseAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CloseAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CloseAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<Account>().GetById(request.Id);
        account.ClosedDate = DateTime.Now;
        await _unitOfWork.Repository<Account>().UpdateAsync(account);

        await _unitOfWork.Save(cancellationToken);
    }
}
