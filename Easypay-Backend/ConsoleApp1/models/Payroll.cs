using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{

    public class Payroll
    {
        public int PayrollID { get; set; }  
        public int EmployeeID { get; set; }  
        public DateTime PayPeriodStart { get; set; }  
        public DateTime PayPeriodEnd { get; set; }  
        public decimal HoursWorked { get; set; }  
        public decimal GrossAmount { get; set; }  
        public decimal Deductions { get; set; }  
        public decimal NetAmount { get; set; }  
        public DateTime PayDate { get; set; }  

        
        public Employee Employee { get; set; }

        
        public Payroll() { }

        
        public Payroll(int employeeId, DateTime payPeriodStart, DateTime payPeriodEnd,
                       decimal hoursWorked, decimal grossAmount, decimal deductions,
                       decimal netAmount, DateTime payDate)
        {
            EmployeeID = employeeId;
            PayPeriodStart = payPeriodStart;
            PayPeriodEnd = payPeriodEnd;
            HoursWorked = hoursWorked;
            GrossAmount = grossAmount;
            Deductions = deductions;
            NetAmount = netAmount;
            PayDate = payDate;
        }

        
        public Payroll(int employeeID, DateTime payDate)
        {
            EmployeeID = employeeID;
            PayDate = payDate;
        }
    }

    
    
}
