using CreditService.Model.Enum;
using System.ComponentModel.DataAnnotations;
using System.Xml.Schema;

namespace CreditService.Model.Entity
{
    public class CreditTariff
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Percent { get; set; } = decimal.Zero;
        public Guid CurrencyId { get; private set; } = Guid.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public RateType rateType { get; set; }
        //необязательный функционал
        public PaymentTypeEnum PaymentType { get; set; }
        public decimal? MaxCreditSum { get; set; }
        public decimal? MinCreditSum { get; set; }

        //минимальный и максимальный срок погашения измеряется в кол-ве дней
        public int? MinRepaymentPeriod { get; set; }
        public int? MaxRepaymentPeriod { get; set; }
        public decimal PennyPercent { get; set; } = decimal.Zero;

    }
}
