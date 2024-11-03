using EasyPay.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Payroll
{
    [Key]
    public int PayrollId { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    [ForeignKey("EmployeeId")]
    public Employee Employee { get; set; }

    [Required]
    public DateTime PayDate { get; set; }

    public ICollection<Earning> Earnings { get; set; } = new List<Earning>();
    public ICollection<Deduction> Deductions { get; set; } = new List<Deduction>();

    //public decimal GrossSalary => Earnings.Sum(e => e.Amount);
    //public decimal DeductionAmount => Deductions.Sum(d => d.Amount);
    //public decimal NetPay => GrossSalary - DeductionAmount;

    public decimal TotalDeductions { get; internal set; }
    public decimal TotalEarnings { get; internal set; }
    public decimal NetAmount { get; internal set; }


}
