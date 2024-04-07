using CreditService.Model.Entity;
using CreditService.Model.Enum;
using System.ComponentModel.DataAnnotations;

namespace CreditService.Model.DTO
{
    public class UserCreditModel
    {
        public Guid Id { get; set; }
        public string Currency { get; set; }
        public StatusEnum Status { get; set; }
        public decimal Value { get; set; }
        public string TariffName { get; set; }
        public int RepaymentPeriod { get; set; }
    }
}
