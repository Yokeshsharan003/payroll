using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class Benefit
    {
        [Key]
        public int BenefitId { get; set; }

        [Required]
        [StringLength(100)]
        public string BenefitName { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Amount { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


       
    }

}
