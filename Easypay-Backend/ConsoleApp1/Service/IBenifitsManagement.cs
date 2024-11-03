using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal interface IBenifitsManagement
    {
        void ManageBenefits();
        void UpdateBenefitInformation();
        //void ApproveLeaveRequest(int managerId, int leaveRequestId);
        //void RequestLeave(int employeeId, LeaveRequest leaveRequest);
    }
}
