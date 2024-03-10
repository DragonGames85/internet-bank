using System.ComponentModel.DataAnnotations;
namespace CreditService.Model.Enum
{
    public enum PaymentTypeEnum
    {
        [Display(Name = ApplicationPaymentType.AnnuityPayment)]
        AnnuityPayment,
        [Display(Name = ApplicationPaymentType.DifferentiatedPayment)]
        DifferentiatedPayment,
    }
    public class ApplicationPaymentType
    {
        public const string AnnuityPayment = "ANNUITY";
        public const string DifferentiatedPayment = "DIFFERENTIATED";
    }
}
