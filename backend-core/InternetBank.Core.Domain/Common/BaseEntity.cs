namespace InternetBank.Core.Domain.Common;

public abstract class BaseEntity : IEntity
{
    public Guid Id { get; protected set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity()
    {
        Id = Guid.NewGuid();
    }
}