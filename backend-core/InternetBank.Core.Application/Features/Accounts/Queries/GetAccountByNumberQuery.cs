using InternetBank.Core.Application.DTOs.AccountDTOs;
using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.DTOs.UserDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;
using System.Xml.Linq;

namespace InternetBank.Core.Application.Features.Accounts.Queries;

public class GetAccountByNumberQuery : IRequest<AccountDto>
{
    public string Number { get; set; }

    public GetAccountByNumberQuery(string number = "")
    {
        Number = number;
    }
}

public class GetAccountByNumberQueryHandler : IRequestHandler<GetAccountByNumberQuery, AccountDto>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAccountByNumberQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<AccountDto> Handle(GetAccountByNumberQuery request, CancellationToken cancellationToken)
    {
        var account = await _unitOfWork.AccountRepository.GetAccountByNumber(request.Number)
            ?? throw new NullReferenceException("Account is not found.");
        
        var dtoCurrency = new CurrencyDto(account.AccountCurrency.Id, account.AccountCurrency.Name, account.AccountCurrency.Symbol);
        var dtoUser = new UserDto(account.CreatedBy ?? Guid.NewGuid(), "");

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
