using casestudy.DAO;
using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal class UserManagement:IUserManagement
    {
        readonly IEasypayRepository _UserManagement;

        public UserManagement()
        {
            _UserManagement = new IEasypayRepositoryImpl();
        }
        public void AddUser()
        {
            Console.WriteLine("Enter User Details: ");

            Console.Write("Username: ");
            string Username = Console.ReadLine();

            Console.Write("password: ");
            string password = Console.ReadLine();

            Console.Write("RoleID: ");
            int RoleID = int.Parse(Console.ReadLine());

            User user = new User(Username, password, RoleID);

            int AddUserStatus = _UserManagement.AddUser(user);
            if (AddUserStatus > 0)
            {
                Console.WriteLine("User Added successfully.");
            }
            else
            {
                Console.WriteLine("Failed to Add User.");


            }

        }
        public void RemoveUser()
        {
            Console.Write("enter the user id: ");
            int UserID = int.Parse(Console.ReadLine());
            int RemoveUserStatus = _UserManagement.RemoveUser(UserID);
            if (RemoveUserStatus > 0)
            {
                Console.WriteLine("User removed successfully.");
            }
            else
            {
                Console.WriteLine("Failed to remove User.");
            }
        }
        public void UpdateUser()
        {
            Console.Write("Enter the User ID to update: ");
            int UserID = int.Parse(Console.ReadLine());

            Console.WriteLine("Enter updated User details:");

            Console.Write("Username: ");
            string Username = Console.ReadLine();

            Console.Write("Password: ");
            string Password = Console.ReadLine();

            Console.Write("RoleID: ");
            int RoleID = int.Parse(Console.ReadLine());

            // Create a new User object with the updated information
            User updatedUser = new User(UserID, Username, Password, RoleID);

            // Call the UpdateUser method from the repository
            int UpdateUserStatus = _UserManagement.UpdateUser(updatedUser);
            if (UpdateUserStatus > 0)
            {
                Console.WriteLine("User updated successfully.");
            }
            else
            {
                Console.WriteLine("Failed to update user.");
            }
        }
    }
}
