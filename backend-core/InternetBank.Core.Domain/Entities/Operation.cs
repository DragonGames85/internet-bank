using InternetBank.Core.Domain.Common;
using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Domain.Entities;

public class Operation : BaseAuditableEntity
{
    public Guid? ReceiveAccountId { get; set; }
    public Guid? SendAccountId { get; set; }
    public Guid CurrencyId { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Value { get; set; }
    public TypeOperation Type { get; set; }

    public Account ReceiveAccount { get; set; }
    public Account SendAccount { get; set; }
    public Currency OperationCurrency { get; set; }

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
