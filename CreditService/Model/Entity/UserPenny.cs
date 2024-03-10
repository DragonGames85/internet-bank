using System.ComponentModel.DataAnnotations;

namespace CreditService.Model.Entity
{
    public class UserPenny
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public decimal Percent { get; set; } = decimal.Zero;
        public int OverdueDays { get; set; } = 0;
    }
}
