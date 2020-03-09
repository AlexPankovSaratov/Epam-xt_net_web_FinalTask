using DalContracts;
using Entities;
using ErrorProcessing;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicContracts
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserDao _userDao;
        public UserLogic(IUserDao userDao)
        {
            _userDao = userDao;
        }
        public bool AddNewUser(string Login, string Password, HashSet<string> Roles)
        {
            try
            {
                if (Login == null || Password == null) return false;
                foreach (User item in _userDao.GetAllUsers())
                {
                    if (item.Login == Login) return false;
                }
                User user = new User { Login = Login, Password = Password };
                if (Roles == null) user.Roles = new HashSet<string>();
                else user.Roles = Roles;
                return _userDao.AddNewUser(user);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return false;
            }
        }

        public bool AddUserRole(int UserID, string RoleName)
        {  
            try
            {
                if (UserID == 0 || RoleName == null || RoleName == "") return false;
                return _userDao.AddUserRole(UserID, RoleName);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return false;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            foreach (User item in _userDao.GetAllUsers())
            {
                yield return item;
            }
        }

        public User GetUserById(int UserID)
        {
            try
            {
                return _userDao.GetUserById(UserID);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public bool RemoveUserRole(int UserID, string RoleName)
        {
            try
            {
                if (UserID == 0 || RoleName == null || RoleName == "") return false;
                return _userDao.RemoveUserRole(UserID, RoleName);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return false;
            }
        }
    }

}
