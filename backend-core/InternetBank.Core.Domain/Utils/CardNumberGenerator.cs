using System.Text;

namespace InternetBank.Core.Domain.Utils;

public static class CardNumberGenerator
{
    public static string Generate()
    {
        var random = new Random();
        var cardNumber = new StringBuilder();

        for (int i = 0; i < 16; i++)
        {
            cardNumber.Append(random.Next(0, 10));
        }

        return cardNumber.ToString();
    }
}
