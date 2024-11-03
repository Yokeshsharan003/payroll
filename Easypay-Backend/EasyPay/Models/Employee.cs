using System.ComponentModel.DataAnnotations;

namespace EasyPay.Models
{

    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(50)]
        public string EmployeeName { get; set; }

        [Required]
        [StringLength(100)]
        public string Designation { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfJoining { get; set; }

        [Required]
        [StringLength(50)]
        public string Department { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal BasicSalary { get; set; }

        [Required]
        [StringLength(100)]
        public string? BankName { get; set; }

        [Required]
        [StringLength(20)]
        public string AccountNumber { get; set; }

        public int GradeId { get; set;}

        public Grade Grade { get; set; }

        //public User User { get; set; }

        public virtual ICollection<Earning> Earning { get; set; } = new List<Earning>();
        public virtual ICollection<Deduction> Deduction { get; set; } = new List<Deduction>();
        public virtual ICollection<Benefit> Benefits { get; set; } = new List<Benefit>();
        public virtual EmployeeDetails EmployeeDetails { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }= new List<Payment>();
        public virtual ICollection<Payroll> Payrolls { get; set; }=new List<Payroll>();
    }


}
