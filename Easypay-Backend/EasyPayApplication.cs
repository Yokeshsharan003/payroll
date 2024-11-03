using casestudy.models;
using casestudy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Remoting.Lifetime;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.EasypayApp
{
    internal class EasyPayApplication
    {
        readonly IEmployeeManagement _EmployeeManagement;
        readonly IUserManagement _UserManagement;
        readonly IPaymentManagement _PaymentManagement;
        readonly ILoginManagement _LoginManagement;
        readonly IBenifitsManagement _BenifitsManagement;
        readonly IPayrollManagement _payrollManagement;
        readonly IComplianceReportManagement _ComplianceReportManagement;

        public EasyPayApplication()
        {
            _EmployeeManagement = new EmployeeManagement();
            _UserManagement = new UserManagement();
            _PaymentManagement = new PaymentManagement();
            //_LoginManagement = new LoginManagement();
            _BenifitsManagement = new BenifitsManagement();
            _payrollManagement = new PayrollManagement();
            _ComplianceReportManagement = new ComplianceReportManagement();
        }

        //public void HandleMainMenu()
        //{
        //    Console.WriteLine("-----Welcome to EasyPay Application--------");

        //    while (true)
        //    {
        //        Console.WriteLine("Would you like to:");
        //        Console.WriteLine("1. Log in");
        //        Console.WriteLine("2. Sign up");
        //        Console.WriteLine("3. Exit");
        //        Console.Write("Please select an option (enter the number): ");

        //        string choice = Console.ReadLine();

        //        switch (choice)
        //        {
        //            case "1":
        //                Login();
        //                break;
        //            case "2":
        //                SignUp();
        //                break;
        //            case "3":
        //                Console.WriteLine("Exiting the system. Goodbye!");
        //                return; // Exit the application
        //            default:
        //                Console.WriteLine("Invalid choice. Please try again.");
        //                break;
        //        }
        //    }
        //}

        //private void Login()
        //{
        //    Console.WriteLine("Enter your username: ");
        //    string username = Console.ReadLine();
        //    Console.WriteLine("Enter your password: ");
        //    string password = Console.ReadLine();

        //    // Call the LoginManagement method to validate credentials
        //    var user = _LoginManagement.ValidateUser(username, password);
        //    if (user != null)
        //    {
        //        Console.WriteLine($"Welcome {username}, you are logged in as {user.Role}.");
        //        DisplayFunctionalities(user.Role);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid credentials.");
        //    }
        //}

        //private void DisplayFunctionalities(Role role)
        //{
        //    throw new NotImplementedException();
        //}

        //private void SignUp()
        //{
        //    Console.WriteLine("Enter your username: ");
        //    string username = Console.ReadLine();
        //    Console.WriteLine("Enter your email: ");
        //    string email = Console.ReadLine();
        //    Console.WriteLine("Enter your password: ");
        //    string password = Console.ReadLine();
        //    Console.WriteLine("Enter your role (Admin, HR Manager, Payroll Processor, Employee, Manager): ");
        //    string role = Console.ReadLine();

        //    // Call the UserManagement method to save the user
        //    bool isSignedUp = _LoginManagement.SignUp(username, email, password, role);
        //    if (isSignedUp)
        //    {
        //        Console.WriteLine("Sign up successful! Please log in.");
        //        Login(); // Proceed to login after successful signup
        //    }
        //    else
        //    {
        //        Console.WriteLine("Sign up failed. Please try again.");
        //    }
        //}

        private void DisplayFunctionalities(string roleName)
        {
            Console.WriteLine($"Functionalities for {roleName}:");

            switch (roleName)
            {
                case "Admin":
                case "HR Manager":
                    Console.WriteLine("• Manage Employee Information");
                    Console.WriteLine("• User Management");
                    Console.WriteLine("• Define Payroll Policies");
                    Console.WriteLine("• Generate Payroll");
                    Console.WriteLine("• Compliance Reporting");
                    break;

                case "Payroll Processor":
                    Console.WriteLine("• Calculate Payroll");
                    Console.WriteLine("• Verify Payroll Data");
                    Console.WriteLine("• Manage Benefits");
                    Console.WriteLine("• Update Benefits Information");
                    Console.WriteLine("• Process Payments");
                    break;

                case "Employee":
                    Console.WriteLine("• View Pay Stubs");
                    Console.WriteLine("• Update Personal Information");
                    Console.WriteLine("• Submit Time Sheets");
                    Console.WriteLine("• Request Leave");
                    break;

                case "Manager":
                case "Supervisor":
                    Console.WriteLine("• Review Team Payrolls");
                    Console.WriteLine("• Approve Leave Requests");
                    break;

                default:
                    Console.WriteLine("No functionalities available for this role.");
                    break;
            }

        }
        
    }
}

