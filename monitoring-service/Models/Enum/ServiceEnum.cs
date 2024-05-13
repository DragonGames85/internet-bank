using System.ComponentModel.DataAnnotations;

namespace monitoring_service.Models.Enum
{
    public enum ServiceEnum
    {
        [Display(Name = ApplicationServiceEnum.Credit)]
        Credit,
        [Display(Name = ApplicationServiceEnum.Core)]
        Core,
        [Display(Name = ApplicationServiceEnum.Auth)]
        Auth,
    }
    public class ApplicationServiceEnum
    {
        public const string Credit = "Credit service";
        public const string Core = "Core service";
        public const string Auth = "Authentication service";
    }
}
