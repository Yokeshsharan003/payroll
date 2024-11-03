﻿using EasyPay.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Deduction
{
    [Key]
    public int DeductionId { get; set; }

    [Required]
    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; }

    [Required]
    public int PayrollId { get; set; }

    [ForeignKey(nameof(PayrollId))]
    public virtual Payroll Payroll { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }
    
    public string Description { get; set; }
}

