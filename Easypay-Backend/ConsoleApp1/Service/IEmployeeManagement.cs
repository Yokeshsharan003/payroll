using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal interface IEmployeeManagement
    {
        void AddEmployee();
        void RemoveEmployee();

        void UpdateEmployee();
        //void SubmitTimeSheet(int employeeId, TimeSheet timeSheet);
    }
}
