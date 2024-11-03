using casestudy.DAO;
using casestudy.models;
using System;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal class PaymentManagement: IPaymentManagement
    {
        readonly IEasypayRepository _PaymentManagement;

        public IEasypayRepositoryImpl PayStubRepository { get; }

        public PaymentManagement()
        {
            _PaymentManagement = new IEasypayRepositoryImpl();
        }

        public PaymentManagement(IEasypayRepositoryImpl payStubRepository)
        {
            PayStubRepository = payStubRepository;
        }

        public void ProcessPayment()
        {
            Console.WriteLine("Enter Payroll ID to process payment: ");
            int payrollID = int.Parse(Console.ReadLine());

            // Retrieve the payroll details for the given PayrollID
            Payroll payroll = _PaymentManagement.GetPayrollByID(payrollID); // Implement this method to get payroll details.

            if (payroll == null)
            {
                Console.WriteLine("Invalid Payroll ID. Please try again.");
                return;
            }

            // Show the amount to be paid
            Console.WriteLine($"Amount due for Payroll ID {payrollID}: {payroll.NetAmount}");

            // Ask for the amount to pay
            Console.WriteLine("Enter the amount you want to pay: ");
            decimal paymentAmount = decimal.Parse(Console.ReadLine());

            // Determine payment status
            if (paymentAmount <= 0)
            {
                Console.WriteLine("Invalid payment amount. Please enter a positive value.");
                return;
            }

            // Handle payment processing
            decimal amountProcessed = Math.Min(paymentAmount, payroll.NetAmount);
            decimal amountPending = payroll.NetAmount - amountProcessed;

            // Call the payment processing logic
            int result = _PaymentManagement.ProcessPayment(payrollID, amountProcessed); // Modify the method to accept the amount.

            if (result > 0)
            {
                Console.WriteLine($"Payment processed successfully: {amountProcessed}");

                if (amountPending > 0)
                {
                    Console.WriteLine($"Amount pending to be processed: {amountPending}");
                }
            }
            else
            {
                Console.WriteLine("Failed to process payment.");
            }
        }
        public void DisplayPayStubs(int employeeId)
        {
            var payStubs = _PaymentManagement.GetPayStubsByEmployeeId(employeeId);

            if (payStubs != null && payStubs.Count > 0)
            {
                Console.WriteLine($"Pay Stubs for Employee ID: {employeeId}");
                foreach (var payStub in payStubs)
                {
                    Console.WriteLine($"PayStub ID: {payStub.PayStubID}, Pay Date: {payStub.PayDate.ToShortDateString()}, Gross Amount: {payStub.GrossAmount:C}, Net Amount: {payStub.NetAmount:C}");
                }
            }
        }


        public List<PayStub> GetPayStubsByEmployeeId(int employeeId)
        {
            if (employeeId <= 0)
            {
                throw new ArgumentException("Invalid Employee ID", nameof(employeeId));
            }

            var payStubs = _PaymentManagement.GetPayStubsByEmployeeId(employeeId);

            if (payStubs == null || payStubs.Count == 0)
            {
                Console.WriteLine("No pay stubs found for the specified employee ID.");
            }

            return payStubs;
        }
    }
}

