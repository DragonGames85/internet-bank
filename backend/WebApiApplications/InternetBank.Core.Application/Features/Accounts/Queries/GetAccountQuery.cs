using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Queries;

public class GetAccountQuery : IRequest<AccountDto>
{
    public Guid Id { get; set; }

    public GetAccountQuery(Guid id)
    {
        Id = id;
    }
}

public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountDto> Handle(GetAccountQuery request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.GetAccountIncludedCurrencyById(request.Id)
            ?? throw new NullReferenceException("Account is not found.");
        
        var dtoCurrency = new CurrencyDto(account.AccountCurrency.Id, account.AccountCurrency.Name, account.AccountCurrency.Symbol);
        var dtoUser = new UserDto(Guid.NewGuid(), "Benjamin Batton");

        return new AccountDto(
            account.Id,
            account.Number,
            account.Balance,
            account.Type,
            account.CreatedDate,
            account.ClosedDate,
            dtoUser,
            dtoCurrency);
    }
}
