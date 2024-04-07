using CreditService.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace CreditService.Model.Entity
{
    public class UserCreditEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Currency { get; set; } = "RUB";
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public StatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public CreditTariff? Tariff { get; set; }
        public Guid TariffId { get; set; }

        //период платежа в кол-ве дней
        public int PaymentPeriod { get; set; } 
        public DateTime DueDate { get; set; } 
        public int RepaymentPeriod { get; set; }

    }
}
