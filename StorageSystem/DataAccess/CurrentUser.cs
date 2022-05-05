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

        public static int Id;

        public static UserRole Role = UserRole.Manager;


    }

    public enum UserRole
    {
        Admin,
        Manager,
        Employee
    }
}
