using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.DAO
{
    public interface IEasypayRepository
    {
        //bool SignUp(string username, string password, int roleId);
        //bool Login(string username, string password);
        int AddEmployee(Employee employee);
        int RemoveEmployee(int EmployeeID);

        int UpdateEmployee(Employee updatedEmployee);
        int AddUser(User user);

        int RemoveUser(int UserID);
        int UpdateUser(User updatedUser);

        int DefinePayrollPolicy(PayrollPolicy payrollpolicy);

       Payroll GeneratePayroll(int employeeID, DateTime payDate);

        List<ComplianceReportItem> GenerateComplianceReport(DateTime startDate, DateTime endDate);

        ///////////////////////////////////////////////////////////////////////////////////////////
        Payroll CalculatePayroll(int employeeID, DateTime payPeriodStart, DateTime payPeriodEnd, decimal hoursWorked, decimal taxRate = 0.2m);
        Payroll VerifyPayrollData(int payrollID);
        int ManageBenefits(Benefit benefit);
        int UpdateBenefitInformation(Benefit benefit);
        int ProcessPayment(int payrollID, decimal amountProcessed);
        Payroll GetPayrollByID(int payrollID);
        List<PayStub> GetPayStubsByEmployeeId(int employeeId);
    }
}
