using InternetBank.Core.Domain.Common;
using InternetBank.Core.Domain.Enum;

namespace InternetBank.Core.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public Guid CurrencyId { get; private set; }
    public decimal Balance { get; private set; } = 0;
    public string Number { get; private set; } = string.Empty;
    public DateTime? ClosedDate { get; private set; }
    public TypeAccount Type { get; private set; }

    protected Account(): base() {}

    public static Account Create(Guid userId, string number, TypeAccount type)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("Parameter of Account \"userId\" can not be empty.");
        if (string.IsNullOrEmpty(number))
            throw new ArgumentException("Parameter of Account \"number\" can not be empty.");

        return new Account()
        {
            Number = number,
            Type = type,
            CreatedBy = userId,
            CreatedDate = DateTime.Now,
        };
    }
}
