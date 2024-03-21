using InternetBank.Core.Application.DTOs.CurrencyDTOs;
using InternetBank.Core.Application.Interfaces.Repositories;
using InternetBank.Core.Domain.Entities;
using MediatR;

namespace InternetBank.Core.Application.Features.Currencies.Queries;

public record GetAllCurrenciesQuery : IRequest<List<CurrencyDto>>
{
}

public class GetAllCurrenciesQueryHandler : IRequestHandler<GetAllCurrenciesQuery, List<CurrencyDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCurrenciesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CurrencyDto>> Handle(GetAllCurrenciesQuery request, CancellationToken cancellationToken)
    {
        var currencies = await _unitOfWork.Repository<Currency>().GetAll();

        var dtoCurrencies = new List<CurrencyDto>();

        foreach (var currency in currencies)
        {
            dtoCurrencies.Add(new CurrencyDto(
                currency.Id,
                currency.Name,
                currency.Symbol));
        }

        return dtoCurrencies;
    }
}
