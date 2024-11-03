using casestudy.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace casestudy.Service
{
    internal interface ILoginManagement
    {
         void Login();
        bool Register();
        bool SignUp();
        User ValidateUser();
    }
}
