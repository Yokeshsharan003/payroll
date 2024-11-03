using casestudy.DAO;
using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal class ComplianceReportManagement:IComplianceReportManagement
    {
        readonly IEasypayRepository _ReportManagement;

        public ComplianceReportManagement()
        {
            _ReportManagement = new IEasypayRepositoryImpl();
        }
        public List<ComplianceReportItem> GenerateComplianceReport()
        {
            Console.WriteLine("Generating Compliance Report...");

            Console.Write("Enter Start Date (yyyy-mm-dd): ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter End Date (yyyy-mm-dd): ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());

            // Fetch employee and payroll data between the specified dates
            var complianceReport = _ReportManagement.GenerateComplianceReport(startDate, endDate);

            if (complianceReport != null && complianceReport.Count > 0)
            {
                Console.WriteLine("Compliance Report Generated Successfully.");
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("EmployeeID | EmployeeName | PayDate | GrossAmount | Deductions | NetAmount");
                Console.WriteLine("------------------------------------------------------");

                foreach (var reportItem in complianceReport)
                {
                    Console.WriteLine($"{reportItem.EmployeeID} | {reportItem.EmployeeName} | {reportItem.PayDate.ToShortDateString()} | " +
                                      $"{reportItem.GrossAmount:C} | {reportItem.Deductions:C} | {reportItem.NetAmount:C}");
                }

                Console.WriteLine("------------------------------------------------------");
            }
            else
            {
                Console.WriteLine("No data found for the specified date range.");
            }
            return complianceReport;
        }
    }
}
