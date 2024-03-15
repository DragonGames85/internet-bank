using InternetBank.Auth.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternetBank.Auth.Domain.Entities;

public class Role : BaseAuditableEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<User> RoleUsers { get; set; } = new List<User>();

    public static Role Create(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentException("Parameter of Role \"name\" can not be empty.");

        return new Role()
        {
            Name = name
        };
    }
}
