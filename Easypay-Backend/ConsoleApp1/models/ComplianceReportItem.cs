using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class ComplianceReportItem
    {
        public int EmployeeID { get; set; }
        public string EmployeeName { get; set; }
        public DateTime PayDate { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetAmount { get; set; }
    }

}
