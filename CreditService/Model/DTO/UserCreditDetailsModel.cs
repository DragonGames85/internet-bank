using CreditService.Model.Entity;
using CreditService.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace CreditService.Model.DTO
{
    public class UserCreditDetailsModel
    {
        public Guid CreditId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public StatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public CreditTariffModel Tariff { get; set; }
        public List<CalculationOfPayments> Payments { get; set; }
        //период платежа в кол-ве дней
        public int PaymentPeriod { get; set; }
        public DateTime DueDate { get; set; }
        public int RepaymentPeriod { get; set; }
    }
}
