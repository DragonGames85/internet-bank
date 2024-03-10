using InternetBank.Core.Domain.Common;

namespace InternetBank.Core.Domain.Entities;

public class Currency : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;

    public ICollection<Operation> CurrencyOperations { get; set; } = new List<Operation>();
    public ICollection<Account> CurrencyAccounts { get; set; } = new List<Account>();

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
