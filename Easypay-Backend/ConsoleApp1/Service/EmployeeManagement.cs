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
    internal class EmployeeManagement:IEmployeeManagement
    {
        readonly IEasypayRepository _EmployeeManagement;

        public EmployeeManagement()
        {
            _EmployeeManagement = new IEasypayRepositoryImpl();
        }
        public void AddEmployee()
        {
            Console.WriteLine("Enter Employee details:");

            Console.Write("FirstName: ");
            string FirstName = Console.ReadLine();

            Console.Write("LastName: ");
            string LastName = Console.ReadLine();

            Console.Write("Email: ");
            string Email = Console.ReadLine();

            Console.Write("phone: ");
            string phone = (Console.ReadLine());

            Console.Write("Address: ");
            string Address = Console.ReadLine();

            Console.Write("HireDate: ");
            DateTime HireDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Department: ");
            string Department = Console.ReadLine();

            Console.Write("Position: ");
            string Position = Console.ReadLine();

            Console.Write("Salary: ");
            decimal Salary = decimal.Parse(Console.ReadLine());


            Console.Write("UserID: ");
            int UserID = int.Parse(Console.ReadLine());

            Employee employee = new Employee(FirstName, LastName, Email, phone, Address, HireDate, Department, Position, Salary, UserID);

            int AddEmployeeStatus = _EmployeeManagement.AddEmployee(employee);
            if (AddEmployeeStatus > 0)
            {
                Console.WriteLine("Employee Added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to Add Employee.");
            }
        }
        public void RemoveEmployee()
        {
            Console.Write("Enter the employee ID: ");
            int employeeID = int.Parse(Console.ReadLine());
            int removeEmployeeStatus = _EmployeeManagement.RemoveEmployee(employeeID);

            if (removeEmployeeStatus > 0)
            {
                Console.WriteLine("Employee removed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove Employee.");
            }
        }
        public void UpdateEmployee()
        {
            Console.Write("Enter the Employee ID to update: ");
            int employeeID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter updated Employee details:");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Address: ");
            string address = Console.ReadLine();

            Console.Write("Hire Date (yyyy-mm-dd): ");
            DateTime hireDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Department: ");
            string department = Console.ReadLine();

            Console.Write("Position: ");
            string position = Console.ReadLine();

            Console.Write("Salary: ");
            decimal salary = decimal.Parse(Console.ReadLine());

            Console.Write("UserID: ");
            int userID = int.Parse(Console.ReadLine());

            // Create a new Employee object with the updated information
            Employee updatedEmployee = new Employee(employeeID, firstName, lastName, email, phone, address, hireDate, department, position, salary, userID);

            // Call the UpdateEmployee method from the repository
            int updateEmployeeStatus = _EmployeeManagement.UpdateEmployee(updatedEmployee);
            if (updateEmployeeStatus > 0)
            {
                Console.WriteLine("Employee updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update employee.");
            }
        }
    }
}
