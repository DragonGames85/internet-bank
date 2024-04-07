using CreditService.Model.Enum;

namespace CreditService.Model.DTO
{
    public class AddCreditTariffModel
    {
        public string Name { get; set; } = string.Empty;
        public decimal Percent { get; set; } = decimal.Zero;
        public string Currency { get; set; }
        public decimal? MaxCreditSum { get; set; }
        public decimal? MinCreditSum { get; set; }
        //минимальный и максимальный срок погашения измеряется в кол-ве месяцев
        public int? MinRepaymentPeriod { get; set; }
        public int? MaxRepaymentPeriod { get; set; }
        public PaymentTypeEnum PaymentType { get; set; }
        public decimal PennyPercent { get; set; } = decimal.Zero;
        public RateType rateType { get; set; }
    }
}
