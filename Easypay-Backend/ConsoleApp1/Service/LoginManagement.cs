using casestudy.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//namespace casestudy.Service
//{
//    using casestudy.models;
//    using System;
//    using System.Collections.Generic;

//    internal class LoginManagement : ILoginManagement
//    {
//        readonly IEasypayRepository _repository;

//        public LoginManagement()
//        {
//            _repository = new IEasypayRepositoryImpl();
//        }

//        public void Login()
//        {
//            Console.WriteLine("Enter your username: ");
//            string username = Console.ReadLine();
//            Console.WriteLine("Enter your password: ");
//            string password = Console.ReadLine();

//            // Call the repository method to validate credentials
//            var user = _repository.ValidateUser(username, password);
//            if (user != null)
//            {
//                Console.WriteLine($"Welcome {username}, you are logged in as {user.Role.RoleName}.");
//                DisplayFunctionalities(user.Role.RoleName);
//            }
//            else
//            {
//                Console.WriteLine("Invalid credentials.");
//            }
//        }

//        public bool Register()
//        {
//            Console.WriteLine("Enter your username: ");
//            string username = Console.ReadLine();
//            Console.WriteLine("Enter your password: ");
//            string password = Console.ReadLine();
//            Console.WriteLine("Enter your role (Admin/HR Manager/Payroll Processor/Employee/Manager/Supervisor): ");
//            string roleName = Console.ReadLine();

//            // Retrieve the role based on the role name
//            var role = _repository.GetRoleByName(roleName);

//            if (role != null)
//            {
//                // Call the repository method to register the user
//                bool registrationSuccess = _repository.RegisterUser(username, password, role.RoleID);
//                if (registrationSuccess)
//                {
//                    Console.WriteLine("Registration successful!");
//                    return true;
//                }
//                else
//                {
//                    Console.WriteLine("Registration failed. Username might already be taken.");
//                    return false;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid role. Registration failed.");
//                return false;
//            }
//        }
//        public Role GetRoleByName(string roleName)
//        {
//            // Find the role in the list by its name
//            return _roles.FirstOrDefault(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
//        }

//        public void DisplayFunctionalities(string roleName)
//        {
//            Console.WriteLine($"Functionalities for {roleName}:");

//            switch (roleName)
//            {
//                case "Admin":
//                case "HR Manager":
//                    Console.WriteLine("• Manage Employee Information");
//                    Console.WriteLine("• User Management");
//                    Console.WriteLine("• Define Payroll Policies");
//                    Console.WriteLine("• Generate Payroll");
//                    Console.WriteLine("• Compliance Reporting");
//                    break;

//                case "Payroll Processor":
//                    Console.WriteLine("• Calculate Payroll");
//                    Console.WriteLine("• Verify Payroll Data");
//                    Console.WriteLine("• Manage Benefits");
//                    Console.WriteLine("• Update Benefits Information");
//                    Console.WriteLine("• Process Payments");
//                    break;

//                case "Employee":
//                    Console.WriteLine("• View Pay Stubs");
//                    Console.WriteLine("• Update Personal Information");
//                    Console.WriteLine("• Submit Time Sheets");
//                    Console.WriteLine("• Request Leave");
//                    break;

//                case "Manager":
//                case "Supervisor":
//                    Console.WriteLine("• Review Team Payrolls");
//                    Console.WriteLine("• Approve Leave Requests");
//                    break;

//                default:
//                    Console.WriteLine("No functionalities available for this role.");
//                    break;
//            }
//        }
//        // In-memory storage for roles
//                    private List<Role> _roles = new List<Role>
//            {
//                new Role { RoleID = 1, RoleName = "Admin" },
//                new Role { RoleID = 2, RoleName = "HR Manager" },
//                new Role { RoleID = 3, RoleName = "Payroll Processor" },
//                new Role { RoleID = 4, RoleName = "Employee" },
//                new Role { RoleID = 5, RoleName = "Manager" },
//                new Role { RoleID = 6, RoleName = "Supervisor" }
//            };

//                    public Role GetRoleByName(string roleName)
//                    {
//                        // Find the role in the list by its name
//                        return _roles.FirstOrDefault(r => r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase));
//                    }
//        // In-memory storage for users
//        private List<User> _users = new List<User>();

//        public bool RegisterUser(string username, string password, int roleId)
//        {
//            // Check if the username already exists
//            if (_users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)))
//            {
//                return false; // Username already taken
//            }

//            // Add the new user to the list
//            _users.Add(new User
//            {
//                UserID = _users.Count + 1, // Simple increment for user ID
//                Username = username,
//                Password = password,
//                RoleID = roleId,
//                Role = _roles.FirstOrDefault(r => r.RoleID == roleId) // Link role to user
//            });

//            return true; // Registration successful
//        }
//        public bool Register()
//        {
//            Console.WriteLine("Enter your username: ");
//            string username = Console.ReadLine();
//            Console.WriteLine("Enter your password: ");
//            string password = Console.ReadLine();
//            Console.WriteLine("Enter your role (Admin/HR Manager/Payroll Processor/Employee/Manager/Supervisor): ");
//            string roleName = Console.ReadLine();

//            // Retrieve the role based on the role name
//            var role = GetRoleByName(roleName);

//            if (role != null)
//            {
//                // Call the RegisterUser method to add the new user
//                bool registrationSuccess = RegisterUser(username, password, role.RoleID);
//                if (registrationSuccess)
//                {
//                    Console.WriteLine("Registration successful!");
//                    return true;
//                }
//                else
//                {
//                    Console.WriteLine("Registration failed. Username might already be taken.");
//                    return false;
//                }
//            }
//            else
//            {
//                Console.WriteLine("Invalid role. Registration failed.");
//                return false;
//            }
//        }


//    }
//}
