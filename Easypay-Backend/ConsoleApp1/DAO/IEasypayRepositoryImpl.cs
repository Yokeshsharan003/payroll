using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using casestudy.models;

namespace casestudy.DAO
{
    public class IEasypayRepositoryImpl : IEasypayRepository
    {
        SqlConnection sqlConnection = null;
        SqlCommand cmd = null;

        public IEasypayRepositoryImpl()
        {
            sqlConnection = new SqlConnection("Server=DESKTOP-BFMCOC6;DataBase=carrentalapp;Trusted_Connection=True");
            cmd = new SqlCommand();
        }
    //    public void SignUp()
    //    {
    //        Console.Write("Enter your username: ");
    //        string username = Console.ReadLine();
    //        Console.Write("Enter your password: ");
    //        string password = Console.ReadLine();
    //        Console.Write("Enter your role (Admin/HR Manager, Payroll Processor, Employee, Manager/Supervisor): ");
    //        string role = Console.ReadLine();

    //        // Validate role input
    //        if (role != "Admin/HR Manager" && role != "Payroll Processor" && role != "Employee" && role != "Manager/Supervisor")
    //        {
    //            Console.WriteLine("Invalid role. Please enter a valid role.");
    //            SignUp(); // Retry signup
    //            return;
    //        }

    //        // Open the database connection
    //        sqlConnection.Open();

    //        // Check if the username already exists
    //        string checkUserQuery = "SELECT COUNT(*) FROM Users WHERE Username = @Username";

    //        cmd.Connection = sqlConnection; // Associate the command with the connection

    //        using (cmd)
    //        {
    //            cmd.CommandText = checkUserQuery;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@Username", username);

    //            int userCount = (int)cmd.ExecuteScalar();

    //            if (userCount > 0)
    //            {
    //                Console.WriteLine("Username already exists. Please choose a different username.");
    //                sqlConnection.Close();
    //                SignUp(); // Retry signup
    //                return;
    //            }
    //        }

    //        // Insert user details into the database
    //        string signUpQuery = @"
    //    INSERT INTO Users (Username, Password, RoleID) 
    //    VALUES (@Username, @Password, (SELECT RoleID FROM Roles WHERE RoleName = @Role));
    //";

    //        using (cmd)
    //        {
    //            cmd.CommandText = signUpQuery;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@Username", username);
    //            cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password
    //            cmd.Parameters.AddWithValue("@Role", role);

    //            cmd.ExecuteNonQuery();
    //            Console.WriteLine("Sign up successful!");
    //        }

    //        sqlConnection.Close();
    //        MainMenu(); // Return to main menu
    //    }
    //    public void Login()
    //    {
    //        Console.Write("Enter your username: ");
    //        string username = Console.ReadLine();
    //        Console.Write("Enter your password: ");
    //        string password = Console.ReadLine();

    //        // Open the database connection
    //        sqlConnection.Open();

    //        // Verify the user credentials
    //        string loginQuery = @"
    //    SELECT RoleID 
    //    FROM Users 
    //    WHERE Username = @Username AND Password = @Password; 
    //";

    //        cmd.Connection = sqlConnection; // Associate the command with the connection

    //        using (cmd)
    //        {
    //            cmd.CommandText = loginQuery;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@Username", username);
    //            cmd.Parameters.AddWithValue("@Password", password); // Consider hashing the password

    //            object result = cmd.ExecuteScalar();

    //            if (result != null)
    //            {
    //                int roleId = (int)result;
    //                string role = GetRoleNameById(roleId); // Retrieve the role name from the RoleID
    //                Console.WriteLine($"Login successful! Welcome, {username}. Your role is: {role}");
    //                // Proceed to the appropriate menu based on role
    //                DisplayMenuByRole(role);
    //            }
    //            else
    //            {
    //                Console.WriteLine("Invalid username or password. Please try again.");
    //                Login(); // Retry login
    //            }
    //        }

    //        sqlConnection.Close();
    //    }
    //    public string GetRoleNameById(int roleId)
    //    {
    //        string roleName = string.Empty;

    //        // Open the database connection
    //        sqlConnection.Open();

    //        string roleQuery = "SELECT RoleName FROM Roles WHERE RoleID = @RoleID;";

    //        using (cmd)
    //        {
    //            cmd.Connection = sqlConnection;
    //            cmd.CommandText = roleQuery;
    //            cmd.Parameters.Clear();
    //            cmd.Parameters.AddWithValue("@RoleID", roleId);

    //            object result = cmd.ExecuteScalar();
    //            roleName = result != null ? result.ToString() : "Unknown Role";
    //        }

    //        sqlConnection.Close();
    //        return roleName;
    //    }
    //    public void DisplayMenuByRole(string role)
    //    {
    //        switch (role)
    //        {
    //            case "Admin/HR Manager":
    //                ShowAdminMenu();
    //                break;
    //            case "Payroll Processor":
    //                ShowPayrollProcessorMenu();
    //                break;
    //            case "Employee":
    //                ShowEmployeeMenu();
    //                break;
    //            case "Manager/Supervisor":
    //                ShowManagerMenu();
    //                break;
    //            default:
    //                Console.WriteLine("Role not recognized. Please contact support.");
    //                break;
    //        }
    //    }

    //    public void ShowAdminMenu()
    //    {
    //        Console.WriteLine("Admin/HR Manager Menu:");
    //        Console.WriteLine("1. Manage Users");
    //        Console.WriteLine("2. View Reports");
    //        Console.WriteLine("3. Update Policies");
    //        // Add more options as needed
    //    }

    //    public void ShowPayrollProcessorMenu()
    //    {
    //        Console.WriteLine("Payroll Processor Menu:");
    //        Console.WriteLine("1. Process Payroll");
    //        Console.WriteLine("2. View Payroll History");
    //        // Add more options as needed
    //    }

    //    public void ShowEmployeeMenu()
    //    {
    //        Console.WriteLine("Employee Menu:");
    //        Console.WriteLine("1. View Payslip");
    //        Console.WriteLine("2. Update Personal Information");
    //        // Add more options as needed
    //    }

    //    public void ShowManagerMenu()
    //    {
    //        Console.WriteLine("Manager/Supervisor Menu:");
    //        Console.WriteLine("1. Approve Timesheets");
    //        Console.WriteLine("2. View Team Performance");
    //        // Add more options as needed
    //    }
    //    public void MainMenu()
    //    {
    //        while (true)
    //        {
    //            Console.Clear();
    //            Console.WriteLine("Welcome to the Payroll System");
    //            Console.WriteLine("Please select an option:");
    //            Console.WriteLine("1. Login");
    //            Console.WriteLine("2. Signup");
    //            Console.WriteLine("3. Exit");

    //            string choice = Console.ReadLine();

    //            switch (choice)
    //            {
    //                case "1":
    //                    Login(); // Call the Login method
    //                    break;
    //                case "2":
    //                    SignUp(); // Call the Signup method
    //                    break;
    //                case "3":
    //                    Console.WriteLine("Exiting the application. Goodbye!");
    //                    return; // Exit the application
    //                default:
    //                    Console.WriteLine("Invalid option. Please try again.");
    //                    break;
    //            }
    //        }
    //    }
    //    public User ValidateUser(string username, string password)
    //    {
    //        try
    //        {
    //            sqlConnection.Open();
    //            cmd.Connection = sqlConnection;
    //            cmd.CommandText = "SELECT u.UserID, u.Username, u.Password, u.RoleID, r.RoleName " +
    //                              "FROM Users u INNER JOIN Roles r ON u.RoleID = r.RoleID " +
    //                              "WHERE u.Username = @username AND u.Password = @password";
    //            cmd.Parameters.AddWithValue("@username", username);
    //            cmd.Parameters.AddWithValue("@password", password);

    //            SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                return new User
    //                {
    //                    UserID = reader.GetInt32(0),
    //                    Username = reader.GetString(1),
    //                    Password = reader.GetString(2),
    //                    RoleID = reader.GetInt32(3),
    //                    Role = new Role { RoleID = reader.GetInt32(3), RoleName = reader.GetString(4) }
    //                };
    //            }
    //            return null; // User not found or invalid credentials
    //        }
    //        finally
    //        {
    //            sqlConnection.Close();
    //            cmd.Parameters.Clear();
    //        }
    //    }

    //    public Role GetRoleByName(string roleName)
    //    {
    //        try
    //        {
    //            sqlConnection.Open();
    //            cmd.Connection = sqlConnection;
    //            cmd.CommandText = "SELECT RoleID, RoleName FROM Roles WHERE RoleName = @roleName";
    //            cmd.Parameters.AddWithValue("@roleName", roleName);

    //            SqlDataReader reader = cmd.ExecuteReader();
    //            if (reader.Read())
    //            {
    //                return new Role
    //                {
    //                    RoleID = reader.GetInt32(0),
    //                    RoleName = reader.GetString(1)
    //                };
    //            }
    //            return null; // Role not found
    //        }
    //        finally
    //        {
    //            sqlConnection.Close();
    //            cmd.Parameters.Clear();
    //        }
    //    }
    //    public bool RegisterUser(string username, string password, int roleId)
    //    {
    //        try
    //        {
    //            sqlConnection.Open();
    //            cmd.Connection = sqlConnection;
    //            cmd.CommandText = "INSERT INTO Users (Username, Password, RoleID) VALUES (@username, @password, @roleId)";
    //            cmd.Parameters.AddWithValue("@username", username);
    //            cmd.Parameters.AddWithValue("@password", password);
    //            cmd.Parameters.AddWithValue("@roleId", roleId);

    //            int rowsAffected = cmd.ExecuteNonQuery();
    //            return rowsAffected > 0;
    //        }
    //        finally
    //        {
    //            sqlConnection.Close();
    //            cmd.Parameters.Clear();
    //        }
    //    }






        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int AddEmployee(Employee employee)
            {
            cmd.CommandText = "INSERT INTO Employees (FirstName,LastName,Email,Phone,Address,HireDate,Department,Position,Salary,UserID) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @HireDate, @Department,@Position,@Salary,@UserID)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Position", employee.Position);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@UserID", employee.UserID);

            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            int CreateEmployeeStatus = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return CreateEmployeeStatus;

        }
        public int RemoveEmployee(int employeeID)
        {
            int removeEmployeeStatus = 0;

            cmd.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();

                cmd.CommandText = "DELETE FROM Payments WHERE PayrollID IN (SELECT PayrollID FROM Payrolls WHERE EmployeeID = @EmployeeID)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();

                // Remove related records in Payrolls table
                cmd.CommandText = "DELETE FROM Payrolls WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();

                // Remove related records in LeaveRequests table
                cmd.CommandText = "DELETE FROM LeaveRequests WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();

                // Remove related records in Benefits table
                cmd.CommandText = "DELETE FROM Benefits WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "DELETE FROM Deductions WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.ExecuteNonQuery();

                // Then, delete the employee
                cmd.CommandText = "DELETE FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                removeEmployeeStatus = cmd.ExecuteNonQuery();

                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return removeEmployeeStatus;
        }


        public int UpdateEmployee(Employee employee)
        {
            int updateEmployeeStatus = 0;

            // Step 1: Check if the employee exists
            cmd.CommandText = "SELECT COUNT(*) FROM Employees WHERE EmployeeID = @EmployeeID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

            sqlConnection.Open();
            cmd.Connection = sqlConnection;

            // Get the count of employees with the given EmployeeID
            int employeeCount = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    employeeCount = reader.GetInt32(0); // Read the count
                }
            }
            sqlConnection.Close();

            // If employee not found, return failure
            if (employeeCount == 0)
            {
                return 0; // Employee does not exist
            }

            // Step 2: Update the employee information
            cmd.CommandText = @"
        UPDATE Employees
        SET FirstName = @FirstName,
            LastName = @LastName,
            Email = @Email,
            Phone = @Phone,
            Address = @Address,
            HireDate = @HireDate,
            Department = @Department,
            Position = @Position,
            Salary = @Salary,
            UserID = @UserID
        WHERE EmployeeID = @EmployeeID";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
            cmd.Parameters.AddWithValue("@LastName", employee.LastName);
            cmd.Parameters.AddWithValue("@Email", employee.Email);
            cmd.Parameters.AddWithValue("@Phone", employee.Phone);
            cmd.Parameters.AddWithValue("@Address", employee.Address);
            cmd.Parameters.AddWithValue("@HireDate", employee.HireDate);
            cmd.Parameters.AddWithValue("@Department", employee.Department);
            cmd.Parameters.AddWithValue("@Position", employee.Position);
            cmd.Parameters.AddWithValue("@Salary", employee.Salary);
            cmd.Parameters.AddWithValue("@UserID", employee.UserID);
            cmd.Parameters.AddWithValue("@EmployeeID", employee.EmployeeID);

            try
            {
                sqlConnection.Open();
                updateEmployeeStatus = cmd.ExecuteNonQuery(); // Execute the update command
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return updateEmployeeStatus; // Return the number of affected rows
        }



        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int AddUser(User user)
        {
            cmd.CommandText = "INSERT INTO Users (Username,Password,RoleID) VALUES (@Username, @Password, @RoleID)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);


            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            int CreateUserStatus = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return CreateUserStatus;

        }

        public int RemoveUser(int userID)
        {
            int removeUserStatus = 0;

            cmd.CommandText = "DELETE FROM Users WHERE UserID = @userID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserID", userID);

            cmd.Connection = sqlConnection;
            try
            {
                sqlConnection.Open();
                removeUserStatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);

            }

            return removeUserStatus;

        }

        public int UpdateUser(User user)
        {
            int updateUserStatus = 0;

            // Step 1: Check if the user exists
            cmd.CommandText = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserID", user.UserID);

           
            sqlConnection.Open();
            cmd.Connection = sqlConnection;
           

            // Get the count of users with the given UserID
            int userCount = 0;
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    userCount = reader.GetInt32(0); // Read the count
                }
            }
            sqlConnection.Close();

            // If user not found, return failure
            if (userCount == 0)
            {
                return 0; // User does not exist
            }

            // Step 2: Update the user information
            cmd.CommandText = @"
                UPDATE Users
                SET Username = @Username,
                    Password = @Password,
                    RoleID = @RoleID
                WHERE UserID = @UserID";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Username", user.Username);
            cmd.Parameters.AddWithValue("@Password", user.Password);
            cmd.Parameters.AddWithValue("@RoleID", user.RoleID);
            cmd.Parameters.AddWithValue("@UserID", user.UserID);

            try
            {
                sqlConnection.Open();
                updateUserStatus = cmd.ExecuteNonQuery(); // Execute the update command
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

            return updateUserStatus; // Return the number of affected rows
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int DefinePayrollPolicy(PayrollPolicy payrollpolicy)
        {
            cmd.CommandText = "INSERT INTO PayrollPolicies (PolicyName,Description,EffectiveDate) VALUES (@PolicyName, @Description, @EffectiveDate)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PolicyName", payrollpolicy.PolicyName);
            cmd.Parameters.AddWithValue("@Description", payrollpolicy.Description);
            cmd.Parameters.AddWithValue("@EffectiveDate", payrollpolicy.EffectiveDate);



            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            int definePayrollStatus = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return definePayrollStatus;

        }
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Payroll GeneratePayroll(int employeeID, DateTime payDate)
        {
            Payroll payroll = null;

            decimal grossAmount = 0;
            decimal deductions = 0;
            decimal netAmount = 0;

            cmd.Connection = sqlConnection;

            try
            {
                // Step 1: Get Employee's Salary
                cmd.CommandText = "SELECT Salary FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                sqlConnection.Open();
                var salaryResult = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (salaryResult != null)
                {
                    grossAmount = (decimal)salaryResult;
                }
                else
                {
                    // If employee not found, return null
                    return null;
                }

                // Step 2: Calculate total benefits for the employee
                cmd.CommandText = "SELECT SUM(Amount) FROM Benefits WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                sqlConnection.Open();
                var benefitsResult = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (benefitsResult != null)
                {
                    grossAmount += (decimal)benefitsResult;
                }

                // Step 3: Calculate total deductions for the employee
                cmd.CommandText = "SELECT SUM(Amount) FROM Deductions WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                sqlConnection.Open();
                var deductionsResult = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (deductionsResult != null)
                {
                    deductions = (decimal)deductionsResult;
                }

                // Step 4: Calculate net amount
                netAmount = grossAmount - deductions;

                // Step 5: Calculate PayPeriodStart and PayPeriodEnd
                DateTime payPeriodStart = payDate.AddDays(-14); // Example: Start 14 days before the pay date
                DateTime payPeriodEnd = payDate; // End date is the pay date
                decimal hoursWorked = 80; // Set hours worked as needed

                // Step 6: Insert the payroll record into the Payrolls table
                cmd.CommandText = "INSERT INTO Payrolls (EmployeeID, PayPeriodStart, PayPeriodEnd, HoursWorked, GrossAmount, Deductions, NetAmount, PayDate) " +
                                  "VALUES (@EmployeeID, @PayPeriodStart, @PayPeriodEnd, @HoursWorked, @GrossAmount, @Deductions, @NetAmount, @PayDate)";

                // Clear parameters before adding new ones
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@PayPeriodStart", payPeriodStart);
                cmd.Parameters.AddWithValue("@PayPeriodEnd", payPeriodEnd);
                cmd.Parameters.AddWithValue("@HoursWorked", hoursWorked);
                cmd.Parameters.AddWithValue("@GrossAmount", grossAmount);
                cmd.Parameters.AddWithValue("@Deductions", deductions);
                cmd.Parameters.AddWithValue("@NetAmount", netAmount); // Ensure this line is included
                cmd.Parameters.AddWithValue("@PayDate", payDate);

                sqlConnection.Open();
                int generatePayrollStatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                if (generatePayrollStatus > 0)
                {
                    payroll = new Payroll
                    {
                        EmployeeID = employeeID,
                        PayPeriodStart = payPeriodStart,
                        PayPeriodEnd = payPeriodEnd,
                        HoursWorked = hoursWorked,
                        PayDate = payDate,
                        GrossAmount = grossAmount,
                        Deductions = deductions,
                        NetAmount = netAmount
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            finally
            {
                if (sqlConnection.State == ConnectionState.Open)
                {
                    sqlConnection.Close(); // Ensure the connection is closed in case of an error
                }
            }

            return payroll;
        }








        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public List<ComplianceReportItem> GenerateComplianceReport(DateTime startDate, DateTime endDate)
        {
            List<ComplianceReportItem> reportItems = new List<ComplianceReportItem>();

            cmd.CommandText = @"
                SELECT e.EmployeeID, 
                       (e.FirstName + ' ' + e.LastName) as EmployeeName, 
                       p.PayDate, 
                       p.GrossAmount, 
                       p.Deductions, 
                       p.NetAmount
                FROM Employees e
                JOIN Payrolls p ON e.EmployeeID = p.EmployeeID
                WHERE p.PayDate BETWEEN @StartDate AND @EndDate";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);

            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                ComplianceReportItem item = new ComplianceReportItem
                {
                    EmployeeID = (int)reader["EmployeeID"],
                    EmployeeName = reader["EmployeeName"].ToString(),
                    PayDate = (DateTime)reader["PayDate"],
                    GrossAmount = (decimal)reader["GrossAmount"],
                    Deductions = (decimal)reader["Deductions"],
                    NetAmount = (decimal)reader["NetAmount"]
                };
                reportItems.Add(item);
            }

            sqlConnection.Close();
            return reportItems;
            
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Payroll CalculatePayroll(int employeeID, DateTime payPeriodStart, DateTime payPeriodEnd, decimal hoursWorked, decimal taxRate = 0.2m)
        {
            Payroll payroll = null;
            decimal grossAmount = 0;
            decimal deductions = 0;
            decimal netAmount = 0;

            cmd.Connection = sqlConnection;

            try
            {
                // Step 1: Get Employee's Salary
                cmd.CommandText = "SELECT Salary FROM Employees WHERE EmployeeID = @EmployeeID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);

                sqlConnection.Open();
                var salaryResult = cmd.ExecuteScalar();
                sqlConnection.Close();

                if (salaryResult != null)
                {
                    grossAmount = (decimal)salaryResult * hoursWorked;
                }
                else
                {
                    // If employee not found, return null
                    return null;
                }

                // Step 2: Calculate tax deducted
                deductions = grossAmount * taxRate;

                // Step 3: Calculate net pay
                netAmount = grossAmount - deductions;

                // Step 4: Insert the payroll record into the Payrolls table
                cmd.CommandText = "INSERT INTO Payrolls (EmployeeID, PayPeriodStart, PayPeriodEnd, HoursWorked, GrossAmount, Deductions, NetAmount, PayDate) " +
                                  "VALUES (@EmployeeID, @PayPeriodStart, @PayPeriodEnd, @HoursWorked, @GrossAmount, @Deductions, @NetAmount, @PayDate)";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@EmployeeID", employeeID);
                cmd.Parameters.AddWithValue("@PayPeriodStart", payPeriodStart);
                cmd.Parameters.AddWithValue("@PayPeriodEnd", payPeriodEnd);
                cmd.Parameters.AddWithValue("@HoursWorked", hoursWorked);
                cmd.Parameters.AddWithValue("@GrossAmount", grossAmount);
                cmd.Parameters.AddWithValue("@Deductions", deductions);
                cmd.Parameters.AddWithValue("@NetPay", netAmount);
                cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);

                sqlConnection.Open();
                int generatePayrollStatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();

                if (generatePayrollStatus > 0)
                {
                    payroll = new Payroll
                    {
                        EmployeeID = employeeID,
                        PayPeriodStart = payPeriodStart,
                        PayPeriodEnd = payPeriodEnd,
                        HoursWorked = hoursWorked,
                        GrossAmount = grossAmount,
                        Deductions = deductions,
                        NetAmount = netAmount,
                        PayDate = DateTime.Now
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return payroll;
        }
        public Payroll VerifyPayrollData(int payrollID)
        {
            Payroll payroll = null;

            cmd.Connection = sqlConnection;

            try
            {
                cmd.CommandText = "SELECT * FROM Payrolls WHERE PayrollID = @PayrollID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@PayrollID", payrollID);

                sqlConnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    payroll = new Payroll
                    {
                        PayrollID = (int)reader["PayrollID"],
                        EmployeeID = (int)reader["EmployeeID"],
                        PayPeriodStart = (DateTime)reader["PayPeriodStart"],
                        PayPeriodEnd = (DateTime)reader["PayPeriodEnd"],
                        HoursWorked = (decimal)reader["HoursWorked"],
                        GrossAmount = (decimal)reader["GrossAmount"],
                        Deductions = (decimal)reader["Deductions"],
                        NetAmount = (decimal)reader["NetAmount"],
                        PayDate = (DateTime)reader["PayDate"]
                    };
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return payroll;
        }
        public int ManageBenefits(Benefit benefit)
        {
            cmd.CommandText = "INSERT INTO Benefits (BenefitType, Amount, EmployeeID,EffectiveDate) VALUES (@BenefitType, @Amount, @EmployeeID,@EffectiveDate)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@BenefitType", benefit.BenefitType);
            cmd.Parameters.AddWithValue("@Amount", benefit.Amount);
            cmd.Parameters.AddWithValue("@EmployeeID", benefit.EmployeeID);
            cmd.Parameters.AddWithValue("@EffectiveDate", benefit.@EffectiveDate);

            cmd.Connection = sqlConnection;
            sqlConnection.Open();
            int addBenefitStatus = cmd.ExecuteNonQuery();
            sqlConnection.Close();
            return addBenefitStatus;
        }
        public int UpdateBenefitInformation(Benefit benefit)
        {
            int updateBenefitStatus = 0;

            cmd.CommandText = "UPDATE Benefits SET BenefitType = @BenefitType, Amount = @Amount,EffectiveDate=@EffectiveDate WHERE BenefitID = @BenefitID";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@BenefitType", benefit.BenefitType);
            cmd.Parameters.AddWithValue("@Amount", benefit.Amount);
            cmd.Parameters.AddWithValue("@BenefitID", benefit.BenefitID);
            cmd.Parameters.AddWithValue("@EffectiveDate", benefit.EffectiveDate);

            cmd.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();
                updateBenefitStatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return updateBenefitStatus;
        }
        public Payroll GetPayrollByID(int payrollID)
        {
            Payroll payroll = null;

            cmd.CommandText = "SELECT EmployeeID, PayPeriodStart, PayPeriodEnd, HoursWorked, GrossAmount, Deductions, NetAmount FROM Payrolls WHERE PayrollID = @PayrollID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PayrollID", payrollID);

            cmd.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payroll = new Payroll
                        {
                            EmployeeID = reader.GetInt32(0),
                            PayPeriodStart = reader.GetDateTime(1),
                            PayPeriodEnd = reader.GetDateTime(2),
                            HoursWorked = reader.GetDecimal(3),
                            GrossAmount = reader.GetDecimal(4),
                            Deductions = reader.GetDecimal(5),
                            NetAmount = reader.GetDecimal(6)
                        };
                    }
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return payroll;
        }

        public int ProcessPayment(int payrollID , decimal amountProcessed)
        {
            int processPaymentStatus = 0;

            cmd.CommandText = "INSERT INTO Payments (PayrollID, PaymentDate, PaymentStatus) VALUES (@PayrollID, @PaymentDate, @PaymentStatus)";

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@PayrollID", payrollID);
            cmd.Parameters.AddWithValue("@PaymentDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@PaymentStatus", "Processed");

            cmd.Connection = sqlConnection;

            try
            {
                sqlConnection.Open();
                processPaymentStatus = cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }

            return processPaymentStatus;
        }

        public List<PayStub> GetPayStubsByEmployeeId(int employeeId)
        {
            List<PayStub> payStubs = new List<PayStub>();
            string query = "SELECT * FROM PayStubs WHERE EmployeeID = @EmployeeID";

            try
            {
                // Open the SQL connection
                sqlConnection.Open();

                // Prepare the SQL command
                using (SqlCommand cmd = new SqlCommand(query, sqlConnection))
                {
                    // Add the parameter to the SQL command
                    cmd.Parameters.AddWithValue("@EmployeeID", employeeId);

                    // Execute the command and retrieve the data
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // Populate the PayStub object and add it to the list
                            PayStub payStub = new PayStub
                            {
                                PayStubID = reader.GetInt32(0),
                                EmployeeID = reader.GetInt32(1),
                                PayDate = reader.GetDateTime(2),
                                GrossAmount = reader.GetDecimal(3),
                                NetAmount = reader.GetDecimal(4)
                            };

                            payStubs.Add(payStub);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that might have occurred
                Console.WriteLine($"An error occurred while retrieving pay stubs: {ex.Message}");
            }
            finally
            {
                // Ensure the connection is closed even if an error occurs
                sqlConnection.Close();
            }

            return payStubs;
        }


    }
}
