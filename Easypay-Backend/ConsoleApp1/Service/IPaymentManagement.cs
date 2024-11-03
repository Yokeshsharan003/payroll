using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal interface IPaymentManagement
    {
        void ProcessPayment();
        
        List<PayStub> GetPayStubsByEmployeeId(int employeeId);
        void DisplayPayStubs(int employeeId);
    }
}
