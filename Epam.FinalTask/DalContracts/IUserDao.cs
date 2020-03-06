using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface IUserDao
    {
        User GetUserById(int UserID);
        IEnumerable<User> GetAllUsers();
        bool AddUserRole(int UserID, string RoleName);
        bool AddNewUser(User NewUser);
        bool RemoveUserRole(int userID, string roleName);
    }
}
