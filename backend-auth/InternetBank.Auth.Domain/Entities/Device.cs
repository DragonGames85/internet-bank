using InternetBank.Auth.Domain.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InternetBank.Auth.Domain.Entities;

public class Device : BaseAuditableEntity
{
    public string Token { get; set; }

    public User User { get; set; }

    public static Device Create(string token)
    {
        return new Device()
        {
            Token = token,
        };
    }
}
