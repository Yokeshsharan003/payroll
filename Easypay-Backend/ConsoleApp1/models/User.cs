using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RoleID { get; set; }
        public DateTime CreatedAt { get; set; }
        public Role Role { get; set; } 

        
        public User() { }

        
        public User(string username, string password, int roleId)
        {
            Username = username;
            Password = password;
            RoleID = roleId;
            CreatedAt = DateTime.Now;
        }

        public User(int userID, string username, string password, int roleID)
        {
            UserID = userID;
            Username = username;
            Password = password;
            RoleID = roleID;
        }

    }


}
