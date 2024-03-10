using System.ComponentModel.DataAnnotations;
namespace CreditService.Model.Enum
{
    public enum RateType
    {
        [Display(Name = ApplicationRateType.Monthly)]
        Monthly,
        [Display(Name = ApplicationRateType.Annual)]
        Annual,
    }
    public class ApplicationRateType
    {
        public const string Monthly = "MONTHLY";
        public const string Annual = "ANNUAL";
    }
}
