using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using InternetBank.Core.Domain.Enumerations;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Commands;

public class EditAccountCommand : IRequest
{
    public Guid Id { get; set; }
    public EditAccountDto Dto { get; set; }
    public EditAccountCommand(Guid id, EditAccountDto dto)
    {
        Id = id;
        Dto = dto;
    }
}

public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public EditAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(EditAccountCommand request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.Repository<Account>().GetById(request.Id);

        if (request.Dto.Number != null)
            account.Number = request.Dto.Number;
        if (request.Dto.Balance != null)
            account.Balance = (decimal)request.Dto.Balance;
        if (request.Dto.Type != null)
            account.Type = (TypeAccount)request.Dto.Type;
        if (request.Dto.CreatedDate != null)
            account.CreatedDate = request.Dto.CreatedDate;
        if (request.Dto.ClosedDate != null)
            account.ClosedDate = request.Dto.ClosedDate;
        if (request.Dto.UserId != null)
            account.CreatedBy = request.Dto.UserId;

        if (request.Dto.CurrencyName != null)
        {
            var currency = await _unitOfWork.CurrencyRepository.GetCurrencyByName(request.Dto.CurrencyName);
            if (currency != null)
                account.AccountCurrency = currency;
        }

        await _unitOfWork.Repository<Account>().UpdateAsync(account);

        await _unitOfWork.Save(cancellationToken);
    }
}
