using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public decimal Salary { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }  

    
        public Employee() { }

      
        public Employee(string firstName, string lastName, string email, string phone,
            string address, DateTime hireDate, string department, string position,
            decimal salary, int userId)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            HireDate = hireDate;
            Department = department;
            Position = position;
            Salary = salary;
            UserID = userId;
        }

        public Employee(int employeeID, string firstName, string lastName, string email, string phone, string address, DateTime hireDate, string department, string position, decimal salary, int userID)
        {
            EmployeeID = employeeID;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
            Address = address;
            HireDate = hireDate;
            Department = department;
            Position = position;
            Salary = salary;
            UserID = userID;
        }
    }

}
