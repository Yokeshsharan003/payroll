using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class PayrollPolicy
    {
        public int PolicyID { get; set; }
        public string PolicyName { get; set; }
        public string Description { get; set; }
        public DateTime EffectiveDate { get; set; }

        
        public PayrollPolicy() { }

        
        public PayrollPolicy(string policyName, string description, DateTime effectiveDate)
        {
            PolicyName = policyName;
            Description = description;
            EffectiveDate = effectiveDate;
        }
    }

}
