using casestudy.DAO;
using casestudy.models;
using casestudy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal class PayrollManagement:IPayrollManagement
    {
         readonly IEasypayRepository _PayrollManagement;

         public PayrollManagement()
         {
             _PayrollManagement = new IEasypayRepositoryImpl();
         }
        public void DefinePayrollPolicy()
        {
            Console.WriteLine("Enter Policy Details: ");
            Console.Write("PolicyName: ");
            string PolicyName = Console.ReadLine();

            Console.Write("Description: ");
            string Description = Console.ReadLine();

            Console.Write("EffectiveDate: ");
            DateTime EffectiveDate = DateTime.Parse(Console.ReadLine());

            PayrollPolicy payrollpolicy = new PayrollPolicy(PolicyName, Description, EffectiveDate);
            int DefinePayrollStatus = _PayrollManagement.DefinePayrollPolicy(payrollpolicy);
            if (DefinePayrollStatus > 0)
            {
                Console.WriteLine("Policy defined successfully.");
            }
            else
            {
                Console.WriteLine("Failed to define policy.");
            }

        }
        public void GeneratePayroll()
        {
            Console.WriteLine("Enter Payroll Generation Details:");

            Console.Write("EmployeeID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.Write("PayDate (yyyy-mm-dd): ");
            DateTime payDate = DateTime.Parse(Console.ReadLine());

            Payroll payroll = _PayrollManagement.GeneratePayroll(employeeID, payDate);
            if (payroll != null)
            {
                Console.WriteLine("Payroll generated successfully.");
                Console.WriteLine($"EmployeeID: {payroll.EmployeeID}");
                Console.WriteLine($"PayPeriod Start: {payroll.PayPeriodStart.ToShortDateString()}");
                Console.WriteLine($"PayPeriod End: {payroll.PayPeriodEnd.ToShortDateString()}");
                Console.WriteLine($"Hours Worked: {payroll.HoursWorked:F2}");
                Console.WriteLine($"PayDate: {payroll.PayDate.ToShortDateString()}");
                Console.WriteLine($"Gross Amount: {payroll.GrossAmount:C}");
                Console.WriteLine($"Deductions: {payroll.Deductions:C}");
                Console.WriteLine($"Net Amount: {payroll.NetAmount:C}");
            }
            else
            {
                Console.WriteLine("Failed to generate payroll.");
            }
        }
        public void CalculatePayroll()
        {
            Console.WriteLine("Enter Employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter Pay Date (yyyy-mm-dd): ");
            DateTime payDate = DateTime.Parse(Console.ReadLine());

            // The pay period start and end are calculated in the GeneratePayroll method.
            // For a bi-weekly payroll, we'll assume the pay period starts 14 days before the pay date.
            DateTime payPeriodStart = payDate.AddDays(-14);
            DateTime payPeriodEnd = payDate;

            Console.WriteLine("Enter Hours Worked: ");
            decimal hoursWorked = decimal.Parse(Console.ReadLine());

            // Call the GeneratePayroll method, assuming it has been modified to not require NetAmount
            Payroll payroll = _PayrollManagement.GeneratePayroll(employeeID, payDate);

            if (payroll != null)
            {
                Console.WriteLine("Payroll calculated successfully:");
                Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                Console.WriteLine($"Pay Period Start: {payroll.PayPeriodStart}");
                Console.WriteLine($"Pay Period End: {payroll.PayPeriodEnd}");
                Console.WriteLine($"Hours Worked: {hoursWorked}");
                Console.WriteLine($"Gross Amount: {payroll.GrossAmount}");
                Console.WriteLine($"Tax Deductions: {payroll.Deductions}");
                Console.WriteLine($"Net Amount: {payroll.NetAmount}");
            }
            else
            {
                Console.WriteLine("Payroll calculation failed.");
            }
        }
        public void VerifyPayrollData()
        {
            Console.WriteLine("Enter Payroll ID to verify: ");
            int payrollID = int.Parse(Console.ReadLine());

            Payroll payroll = _PayrollManagement.VerifyPayrollData(payrollID);

            if (payroll != null)
            {
                Console.WriteLine("Payroll data:");
                Console.WriteLine($"Employee ID: {payroll.EmployeeID}");
                Console.WriteLine($"Gross Pay: {payroll.GrossAmount}");
                Console.WriteLine($"Tax Deducted: {payroll.Deductions}");
                Console.WriteLine($"Net Pay: {payroll.NetAmount}");
            }
            else
            {
                Console.WriteLine("No payroll data found for the given Payroll ID.");
            }
        }
    }
}
