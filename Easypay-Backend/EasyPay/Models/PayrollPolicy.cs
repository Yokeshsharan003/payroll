using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class PayrollPolicy
    {
        [Key]
        public int PayrollPolicyId { get; set; }

        [Required]
        [StringLength(100)]
        public string? PolicyName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal EPFPercentage { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal ProfessionalTax { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal HealthInsurance { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal TaxPercentage { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal HRAAllowancePercentage { get; set; }

        [Required]
        [Range(0, 100)]
        public decimal BonusPercentage { get; set; }
    }
}
