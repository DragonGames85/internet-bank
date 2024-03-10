using InternetBank.Auth.Domain.Common;

namespace InternetBank.Auth.Domain.Entities;

public class Role : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<User> RoleUsers { get; set; } = new List<User>();
}
