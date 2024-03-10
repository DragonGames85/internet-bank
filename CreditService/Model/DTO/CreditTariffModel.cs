using CreditService.Model.Enum;

namespace CreditService.Model.DTO
{
    public class CreditTariffModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Percent { get; set; } = decimal.Zero;
        public decimal? MaxCreditSum { get; set; }
        public decimal? MinCreditSum { get; set; }
        //минимальный и максимальный срок погашения измеряется в кол-ве месяцев
        public int? MinRepaymentPeriod { get; set; }
        public int? MaxRepaymentPeriod { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public decimal PennyPercent { get; set; } = decimal.Zero;
    }
}
