using InternetBank.Auth.Domain.Common;

namespace InternetBank.Auth.Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Login { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsBanned { get; set; } = false;

    public ICollection<Role> UserRoles { get; set; } = new List<Role>();

    public static User Create(string login, string name, string password)
    {
        // TODO: Add validations

        return new User()
        {
            Login = login,
            Name = name,
            Password = password,
        };
    }
}
