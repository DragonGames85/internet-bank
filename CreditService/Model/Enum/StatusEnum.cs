using System.ComponentModel.DataAnnotations;
namespace CreditService.Model.Enum
{
    public enum StatusEnum
    {
        [Display(Name = ApplicationStatusNames.Opened)]
        Opened,
        [Display(Name = ApplicationStatusNames.Closed)]
        Closed,
    }
    public class ApplicationStatusNames
    {
        public const string Opened = "OPENED";
        public const string Closed = "CLOSED";
    }
}
