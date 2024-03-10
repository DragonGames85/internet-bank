using CreditService.Model.Entity;
using CreditService.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace CreditService.Model.DTO
{
    public class CreditModel
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public Guid Currency { get; set; } = Guid.Empty;
        public decimal Value { get; set; }
        public Guid TariffId { get; set; }

        //период платежа в кол-ве дней
        public int PaymentPeriod { get; set; }
        public int RepaymentPeriod { get; set; }
    }
}
