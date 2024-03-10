using Azure;

namespace CreditService.Model.Entity
{
    public class Currency
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; private set; } = string.Empty;
        public string Symbol { get; private set; } = string.Empty;


        protected Currency() : base() { }

        public static Currency Create(string name, string symbol)
        {
            return new Currency()
            {
                Name = name,
                Symbol = symbol,
            };
        }
    }
}
