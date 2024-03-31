using InternetBank.Auth.Application.DTOs.UserDTOs;
using InternetBank.Auth.Application.Interfaces.Repositories;
using InternetBank.Auth.Domain.Entities;
using MediatR;

namespace InternetBank.Auth.Application.Features.Settings.Commands;

public class AddHideAccountCommand : IRequest
{
    public Guid UserId { get; set; }
    public Guid AccountId { get; set; }
    public AddHideAccountCommand(Guid userId, Guid accountId)
    {
        UserId = userId;
        AccountId = accountId;
    }
}

public class AddHideAccountCommandHandler : IRequestHandler<AddHideAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AddHideAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(AddHideAccountCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Repository<User>().GetById(request.UserId)
            ?? throw new Exception($"User \"{request.UserId}\" is not found.");

        var hideAccount = HideAccount.Create(request.AccountId);
        hideAccount.HideAccountUser = user;

        await _unitOfWork.Repository<HideAccount>().AddAsync(hideAccount);

        await _unitOfWork.Save(cancellationToken);
    }
}
