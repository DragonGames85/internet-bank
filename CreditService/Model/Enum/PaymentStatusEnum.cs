using System.ComponentModel.DataAnnotations;
namespace CreditService.Model.Enum
{
    public enum PaymentStatusEnum
    {
        [Display(Name = ApplicationPaymentStatus.Paid)]
        Paid,
        [Display(Name = ApplicationPaymentStatus.Waiting)]
        Waiting,
        [Display(Name = ApplicationPaymentStatus.Overdue)]
        Overdue,
    }
    public class ApplicationPaymentStatus
    {
        public const string Paid = "PAID";
        public const string Waiting = "WAITING";
        public const string Overdue = "OVERDUE";
    }
}
