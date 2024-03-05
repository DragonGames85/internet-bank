using InternetBank.Core.Domain.Common;
using InternetBank.Core.Domain.Enumerations;

namespace InternetBank.Core.Domain.Entities;

public class Account : BaseAuditableEntity
{
    public Guid CurrencyId { get; private set; }
    public decimal Balance { get; private set; } = 0;
    public string Number { get; private set; } = string.Empty;
    public DateTime? ClosedDate { get; private set; }
    public TypeAccount Type { get; private set; }

    public Currency AccountCurrency { get; set; }
    public ICollection<Operation> ReceivedOperations { get; set; } = new List<Operation>();
    public ICollection<Operation> SentOperations { get; set; } = new List<Operation>();

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
