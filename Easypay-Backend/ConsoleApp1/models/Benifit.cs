using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class Benefit
{
    public int BenefitID { get; set; }
    public int EmployeeID { get; set; }
    public string BenefitType { get; set; }
    public decimal Amount { get; set; }
    public DateTime EffectiveDate { get; set; }
    public Employee Employee { get; set; }  

    
    public Benefit() {}

    
    public Benefit(int employeeId, string benefitType, decimal amount, DateTime effectiveDate)
    {
        EmployeeID = employeeId;
        BenefitType = benefitType;
        Amount = amount;
        EffectiveDate = effectiveDate;
    }
}

}
