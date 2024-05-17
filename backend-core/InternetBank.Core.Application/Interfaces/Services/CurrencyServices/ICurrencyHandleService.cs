using InternetBank.Core.Application.DTOs.CurrencyDTOs;

namespace InternetBank.Core.Application.Interfaces.Services.CurrencyServices;

public interface ICurrencyHandleService
{
    Task CreateCurrency(ShortCurrencyDto dto);
    Task CreateAllCurrency();
    Task EditCurrency(Guid id, ShortCurrencyDto dto);
    Task DeleteCurrency(Guid id);
}
