namespace InternetBank.Core.Domain.Common;

public class BaseAuditableEntity : BaseEntity, IAuditableEntity
{
    protected BaseAuditableEntity(Guid id) : base(id)
    {
    }
    protected BaseAuditableEntity() : base() { }

    public Guid? CreatedBy { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
}
