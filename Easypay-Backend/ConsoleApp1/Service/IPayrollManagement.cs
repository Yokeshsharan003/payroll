using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal interface IPayrollManagement
    {
        void DefinePayrollPolicy();
        void GeneratePayroll();
        void CalculatePayroll();
        void VerifyPayrollData();
    }
}
