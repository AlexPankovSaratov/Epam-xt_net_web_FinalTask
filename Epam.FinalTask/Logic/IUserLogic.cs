using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IUserLogic
    {
        User GetUserById(int UserID);
        IEnumerable<User> GetAllUsers();
        bool AddUserRole(int UserID, string RoleName);
        bool AddNewUser(string Login, string Password, HashSet<string> Roles);
    }
}
