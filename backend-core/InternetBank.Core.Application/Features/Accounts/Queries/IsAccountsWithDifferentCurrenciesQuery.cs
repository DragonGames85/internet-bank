using InternetBank.Core.Application.Interfaces.Repositories;
using MediatR;

namespace InternetBank.Core.Application.Features.Accounts.Queries;

public class IsAccountsWithDifferentCurrenciesQuery : IRequest<bool>
{
    public string FirstAccountNumber { get; set; } = string.Empty;
    public string SecondAccountNumber { get; set; } = string.Empty;

    public IsAccountsWithDifferentCurrenciesQuery(string firstAccountNumber, string secondAccountNumber)
    {
        FirstAccountNumber = firstAccountNumber;
        SecondAccountNumber = secondAccountNumber;
    }
}

public class IsAccountsWithDifferentCurrenciesQueryHandler : IRequestHandler<IsAccountsWithDifferentCurrenciesQuery, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public IsAccountsWithDifferentCurrenciesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(IsAccountsWithDifferentCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var firstAccount = await _unitOfWork.AccountRepository.GetAccountByNumber(request.FirstAccountNumber);
        var secondAccount = await _unitOfWork.AccountRepository.GetAccountByNumber(request.SecondAccountNumber);

        if (firstAccount != null && secondAccount != null && firstAccount.AccountCurrency.Name != secondAccount.AccountCurrency.Name)
            return true;
        return false;
    }
}
