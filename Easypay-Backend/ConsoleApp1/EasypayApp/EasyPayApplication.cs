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
        readonly IHRManagement _HRManagement;
        readonly IPayrollProcessorManagement _PPManagement;

        public EasyPayApplication()
        {
            _HRManagement = new HRManagement();
            _PPManagement = new PayrollProcessorManagement();

        }
        public void HandleMainMenu()
        {

            Console.WriteLine("-----Welcome to EasyPay Application--------");
            Console.WriteLine("Please Login/Register");

            while (true)
            {
            loginPage:
                Console.WriteLine("1.Login as admin/HR \n2.Login as PayrollProcessor \n3.Login as Employee \n4.Login as Manager \n5.Exit");
                int userChoice = int.Parse(Console.ReadLine());

                switch (userChoice)
                {
                    case 1:
                        {
                            Console.WriteLine("Welcome to Login Page");
                            int isAdminLogin = _HRManagement.HRLogin();
                            if (isAdminLogin > 0)
                            {
                                while (true)
                                {
                                mainmenu:
                                    Console.WriteLine("-------Main Menu-------");
                                    Console.WriteLine("1.Employee Management \n2.User Management \n3.PayRoll Management \n4.Generate Compliance Report \n5.Exit");
                                    Console.WriteLine("enter your choice:");
                                    int choice = int.Parse(Console.ReadLine());
                                    switch (choice)
                                    {
                                        case 1:
                                            while (true)
                                            {

                                                Console.WriteLine("------Employee Management------");
                                                Console.WriteLine("1.Add Employee \n2.Remove Employee \n3.Update Employee Information\n4.Main Menu");
                                                Console.WriteLine("enter your choice:");
                                                int choice1 = int.Parse(Console.ReadLine());
                                                switch (choice1)
                                                {
                                                    case 1:
                                                        {

                                                            _HRManagement.AddEmployee();
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            _HRManagement.RemoveEmployee();
                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            _HRManagement.UpdateEmployee();

                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            goto mainmenu;


                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("INVALID CHOICE");
                                                            break;
                                                        }
                                                }
                                            }
                                        case 2:
                                            while (true)
                                            {

                                                Console.WriteLine("------User Management------");
                                                Console.WriteLine("enter your choice:");
                                                Console.WriteLine("1.Add User \n2.Remove User \n3.Update User \n4.Main Menu");
                                                int choice3 = int.Parse(Console.ReadLine());
                                                switch (choice3)
                                                {
                                                    case 1:
                                                        {

                                                            _HRManagement.AddUser();
                                                            break;
                                                        }
                                                    case 2:
                                                        {

                                                            _HRManagement.RemoveUser();

                                                            break;
                                                        }
                                                    case 3:
                                                        {

                                                            _HRManagement.UpdateUser();
                                                            break;
                                                        }
                                                    case 4:
                                                        {
                                                            goto mainmenu;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("INVALID CHOICE");
                                                            break;
                                                        }
                                                }
                                            }
                                        case 3:
                                            while (true)
                                            {
                                                Console.WriteLine("-------------PayRoll Management--------------");
                                                Console.WriteLine("enter your choice:");
                                                Console.WriteLine("1.Define Payroll policies \n2.Generate Payroll \n3.Main Menu");
                                                int choice4 = int.Parse(Console.ReadLine());
                                                switch (choice4)
                                                {
                                                    case 1:
                                                        {

                                                            _HRManagement.DefinePayrollPolicy();

                                                            break;
                                                        }
                                                    case 2:
                                                        {

                                                            _HRManagement.GeneratePayroll();

                                                            break;
                                                        }
                                                    case 3:
                                                        {
                                                            goto mainmenu;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("your choice" + choice4);
                                                            Console.WriteLine("INALID CHOICE");

                                                            break;
                                                        }
                                                }
                                            }
                                        case 4:
                                            while (true)
                                            {
                                                Console.WriteLine("---------Generate Compliance Report-----------");
                                                Console.WriteLine("1.Generate Report \n2.Main Menu");
                                                int choice5 = int.Parse(Console.ReadLine());
                                                switch (choice5)
                                                {
                                                    case 1:
                                                        {
                                                            List<ComplianceReportItem> ComplianceReportItems = _HRManagement.GenerateComplianceReport();
                                                            foreach (var ComplianceReportItem in ComplianceReportItems)
                                                            {
                                                                Console.WriteLine(ComplianceReportItem);
                                                            }
                                                            break;
                                                        }
                                                    case 2:
                                                        {
                                                            goto mainmenu;
                                                        }
                                                    default:
                                                        {
                                                            Console.WriteLine("INVALID CHOICE");
                                                            break;
                                                        }
                                                }
                                            }
                                        case 5:
                                            {
                                                Console.WriteLine("Exit");
                                                Environment.Exit(0);
                                                break;
                                            }

                                    }
                                }
                            }
                            else
                            {

                                goto loginPage;
                            }


                        }

                    case 2:
                        {
                            Console.WriteLine("Welcome to Login Page");
                            int isProcessorLogin = _PPManagement.PPLogin();
                            if (isProcessorLogin > 0)
                            {
                                while (true)
                                {
                                    Console.WriteLine("-------Payroll Processor Menu-------");
                                    Console.WriteLine("1.Calculate Payroll \n2.Verify Payroll Data \n3.Manage Benefits \n4.Update Benefits Information \n5.Process Payments \n6.Exit");
                                    Console.WriteLine("Enter your choice:");
                                    int choice = int.Parse(Console.ReadLine());

                                    switch (choice)
                                    {
                                        case 1:
                                            _PPManagement.CalculatePayroll();
                                            break;

                                        case 2:
                                            _PPManagement.VerifyPayrollData();
                                            break;

                                        case 3:
                                            _PPManagement.ManageBenefits();
                                            break;

                                        case 4:
                                            _PPManagement.UpdateBenefitInformation();
                                            break;

                                        case 5:
                                            Console.WriteLine("Enter Payroll ID to process payment:");
                                            int payrollID = int.Parse(Console.ReadLine());
                                            _PPManagement.ProcessPayment();
                                            break;

                                        case 6:
                                            Console.WriteLine("Exiting...");
                                            return; // Exit the Payroll Processor menu

                                        default:
                                            Console.WriteLine("INVALID CHOICE");
                                            break;
                                    }
                                }
                            }
                            else
                            {

                                goto loginPage;
                            }
                        }
                }
            }
        }
    }
}
