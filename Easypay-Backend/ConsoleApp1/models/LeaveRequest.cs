using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class LeaveRequest
    {
        public int LeaveID { get; set; }
        public int EmployeeID { get; set; }
        public string LeaveType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public Employee Employee { get; set; }  

       
        public LeaveRequest() { }

        public LeaveRequest(int employeeId, string leaveType, DateTime startDate,
            DateTime endDate, string status)
        {
            EmployeeID = employeeId;
            LeaveType = leaveType;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }
    }

}
