using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class PayStub
    {
        public int PayStubID { get; set; }  
        public int EmployeeID { get; set; } 
        public DateTime PayDate { get; set; } 
        public decimal GrossAmount { get; set; } 
        public decimal NetAmount { get; set; } 
        public decimal? TaxAmount { get; set; } 
        public decimal? Deductions { get; set; } 
        public DateTime CreatedAt { get; set; } 

        
        public Employee Employee { get; set; }

       
        public PayStub() { }

        
        public PayStub(int employeeID, DateTime payDate, decimal grossAmount, decimal netAmount, decimal? taxAmount, decimal? deductions)
        {
            EmployeeID = employeeID;
            PayDate = payDate;
            GrossAmount = grossAmount;
            NetAmount = netAmount;
            TaxAmount = taxAmount;
            Deductions = deductions;
            CreatedAt = DateTime.Now;
        }
    }

}
