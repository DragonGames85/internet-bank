using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Settings.Commands;

public class DeleteHideAccountCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public DeleteHideAccountCommand(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

public class DeleteHideAccountCommandHandler : IRequestHandler<DeleteHideAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteHideAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteHideAccountCommand request, CancellationToken cancellationToken)
    {
        var hideAccount = await _unitOfWork.HideAccountRepository.GetHideAccountByAccountId(request.AccountId)
            ?? throw new NullReferenceException("AccountId is not found.");

        await _unitOfWork.Repository<HideAccount>().DeleteAsync(hideAccount);

        await _unitOfWork.Save(cancellationToken);
    }
}
