using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using InternetBank.Core.Domain.Utils;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Commands;

public class CreateAccountCommand : IRequest
{
    public CreateAccountDto Dto { get; set; }
    public Guid UserId { get; set; }
    public int Value { get; set; }
    public CreateAccountCommand(CreateAccountDto dto, Guid userId, int? value = 0)
    {
        Dto = dto;
        UserId = userId;
        Value = value ?? 0;
    }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateAccountCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var currency = await _unitOfWork.CurrencyRepository.GetCurrencyByName(request.Dto.CurrencyName)
            ?? throw new NullReferenceException("Currency is not found.");
        
        var account = Account.Create(
            request.UserId,
            CardNumberGenerator.Generate(),
            request.Dto.Type);

        account.Balance = request.Value;
        account.AccountCurrency = currency;

        await _unitOfWork.Repository<Account>().AddAsync(account);

        await _unitOfWork.Save(cancellationToken);
    }
}
