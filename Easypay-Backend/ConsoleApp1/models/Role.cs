using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.models
{
    public class Role
    {
        internal static Role roleName;

        public int RoleID { get; set; }
        public string RoleName { get; set; }

        
        public Role() { }

       
        public Role(string roleName)
        {
            RoleName = roleName;
        }
    }



}
