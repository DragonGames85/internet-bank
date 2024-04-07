namespace InternetBank.Core.Infrastructure.Refit.Models.Cbr;

public record CbrCurrencyConvert(
    string Desclaimer,
    string Date,
    long Timestamp,
    string Base,
    Dictionary<string, decimal> Rates);
