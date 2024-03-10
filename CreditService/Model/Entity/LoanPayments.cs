using CreditService.Model.Enum;

namespace CreditService.Model.Entity
{
    public class LoanPayments
    {
        public Guid Id { get; set; } = Guid.NewGuid(); 
        public DateTime Date { get; set; }
        public Guid CreditId { get; set; }
        public decimal AmountOfPayment { get; set; }
        public PaymentStatusEnum Status { get; set; }
        public int NumberPay { get; set; }
        public decimal BalanceOwed { get; set; }
        public decimal PercentageForPeriod { get; set; }
        public decimal MainDebt { get; set; }

    }
}
