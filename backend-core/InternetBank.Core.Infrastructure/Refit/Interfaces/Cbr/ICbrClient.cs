using InternetBank.Core.Infrastructure.Refit.Models.Cbr;
using Refit;

namespace InternetBank.Core.Infrastructure.Refit.Interfaces.Cbr;

public interface ICbrClient
{
    [Get("/latest.js")]
    Task<CbrCurrencyConvert> GetCurrencyConvert();
}
