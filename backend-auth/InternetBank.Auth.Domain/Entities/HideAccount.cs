using InternetBank.Auth.Domain.Common;

namespace InternetBank.Auth.Domain.Entities;

public class HideAccount : BaseAuditableEntity
{
    public Guid AccountId { get; set; }

    public User HideAccountUser { get; set; }

    public static HideAccount Create(Guid id)
    {
        return new HideAccount()
        {
            AccountId = id
        };
    }
}
