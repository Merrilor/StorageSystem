using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StorageSystem.Model;

namespace StorageSystem.DataAccess
{
    public static class CurrentUser
    {

        public static string Login;

        public static int Id = 1;

        public static UserRole Role;

        public static string UserImagePath = "/images/user/placeholderUser.jpg"; 


    }

    public enum UserRole
    {
        Admin,
        Manager,
        Employee
    }
}
