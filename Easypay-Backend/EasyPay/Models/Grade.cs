using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }//1-4

        [Required]
        [StringLength(50)]
        public string GradeName { get; set; }

        // Foreign key for PayrollPolicy
        [Required]
        public int PayrollPolicyId { get; set; }
        public PayrollPolicy PayrollPolicy { get; set; }

        // Navigation property for Employees
        public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
