using InternetBank.Core.Application.DTOs.CurrencyDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.CurrencyServices;

public interface ICurrencyGetService
{
    Task<List<CurrencyDto>> GetCurrencies();
}
