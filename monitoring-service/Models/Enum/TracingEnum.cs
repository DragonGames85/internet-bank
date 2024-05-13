using System.ComponentModel.DataAnnotations;

namespace monitoring_service.Models.Enum
{
    public enum TracingEnum
    {
        [Display(Name = ApplicationTracingEnum.Exception)]
        Exception,
        [Display(Name = ApplicationTracingEnum.Logging)]
        Logging,
    }
    public class ApplicationTracingEnum
    {
        public const string Exception = "Exception";
        public const string Logging = "Logging";
    }
}
