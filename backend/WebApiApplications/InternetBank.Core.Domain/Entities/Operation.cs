using InternetBank.Core.Domain.Common;
using InternetBank.Core.Domain.Enum;

namespace InternetBank.Core.Domain.Entities;

public class Operation : BaseAuditableEntity
{
    public Guid? ReceiveAccountId { get; private set; }
    public Guid? SendAccountId { get; private set; }
    public Guid CurrencyId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public decimal Value { get; private set; }
    public TypeOperation Type { get; private set; }

    protected Operation() : base() {}

    public static Operation Create(Guid? ReceiveAccountId, Guid? SendAccountId, string name, decimal value, TypeOperation type)
    {
        if (ReceiveAccountId == null && SendAccountId == null)
            throw new ArgumentException("Parameters of Account \"receiveAccountId\" and \"sendAccountId\" can not be null together.");
        if (value <= 0)
            throw new ArgumentException("Parameter of Account \"value\" can not be zero or negative number.");
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Parameter of Account \"name\" can not be empty.");

        return new Operation()
        {
            Name = name,
            Value = value,
            Type = type,
            CreatedDate = DateTime.Now,
        };
    }
}
