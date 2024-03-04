using InternetBank.Core.Domain.Common;

namespace InternetBank.Core.Domain.Entities;

public class Currency : BaseAuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Symbol { get; private set; } = string.Empty;

    protected Currency() : base() { }

    public static Currency Create(string name, string symbol)
    {
        return new Currency()
        {
            Name = name,
            Symbol = symbol,
        };
    }
}
