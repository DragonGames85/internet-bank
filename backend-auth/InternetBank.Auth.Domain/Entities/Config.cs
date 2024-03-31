using InternetBank.Auth.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternetBank.Auth.Domain.Entities;

public class Config : BaseAuditableEntity
{
    public bool IsLightTheme { get; set; } = true;
    public Guid UserId { get; set; }

    public User User { get; set; }

    public static Config Create(bool isLightTheme)
    {
        return new Config()
        {
            IsLightTheme = isLightTheme
        };
    }
}
