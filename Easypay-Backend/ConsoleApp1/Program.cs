using casestudy.DAO;
using casestudy.EasypayApp;
using casestudy.models;
using casestudy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EasyPayApplication repository = new EasyPayApplication();
         
                // Assume the database connection and repository are already set up.
                IEasypayRepositoryImpl payStubRepository = new IEasypayRepositoryImpl(); // This contains the actual database call.
                IPaymentManagement paymentManagement = new PaymentManagement(payStubRepository); // This contains the service logic.

                // Test the method by requesting an employee ID
                Console.WriteLine("Enter Employee ID to retrieve pay stubs:");
                int employeeId = int.Parse(Console.ReadLine());

                // Display the pay stubs
                paymentManagement.DisplayPayStubs(employeeId);

                // Optionally, you can also call the GetPayStubsByEmployeeId directly
                var payStubs = paymentManagement.GetPayStubsByEmployeeId(employeeId);

                // If you need to process or display the pay stubs further
                if (payStubs != null && payStubs.Count > 0)
                {
                    foreach (var payStub in payStubs)
                    {
                        Console.WriteLine($"PayStub ID: {payStub.PayStubID}, Pay Date: {payStub.PayDate.ToShortDateString()}, Gross Amount: {payStub.GrossAmount:C}, Net Amount: {payStub.NetAmount:C}");
                    }
                }
                else
                {
                    Console.WriteLine("No pay stubs found.");
                }
            }
        }

    }
