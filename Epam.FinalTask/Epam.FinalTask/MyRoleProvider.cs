using Entities;
using Loc;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Epam.FinalTask
{
    public class MyRoleProvider : RoleProvider
    {
        private static IUserLogic UserLogic;
        static MyRoleProvider()
        {
            UserLogic = DependencyResolver.UserLogic;
        }
        public static bool VerifUser(string Login, string Password)
        {
            if (UserLogic.GetAllUsers().Count() == 0)
            {
                //Создаём гостевую учётку
                UserLogic.AddNewUser("", "",null);
            }
            //if (Login == "" && Password != "") return false;
            foreach (User item in UserLogic.GetAllUsers())
            {
                if (item.Login == Login)
                {
                    return item.Password == Password;
                }
            }
            //Первый зарегистрированный пользователь становится админом
            if (UserLogic.GetAllUsers().Count() == 2 && Login != "" && Password != "") UserLogic.AddNewUser(Login, Password, new HashSet<string> { "Admin" });
            else UserLogic.AddNewUser(Login, Password,null);
            return true;
        }
        public static bool AddUserRole(int UserId, string RoleName)
        {
            if (UserId == 0 && RoleName != "") return false;
            if (UserLogic.GetAllUsers().Count() < 1)
            {
                AddUserRole(UserId, "Admin");
            }
            foreach (User item in UserLogic.GetAllUsers())
            {
                if (item.Id == UserId)
                {
                    return UserLogic.AddUserRole(UserId, RoleName);
                }
            }
            return false;
        }
        public override bool IsUserInRole(string Login, string roleName)
        {
            if (Login == "Alex" && roleName == "Admin") return true;
            return false;
        }
        public override string[] GetRolesForUser(string Login)
        {
            string[] Roles = new string[] { };
            foreach (User item in UserLogic.GetAllUsers())
            {
                if (item.Login == Login)
                {
                    Roles = item.Roles.ToArray(); ;
                }
            }
            return Roles;
        }
        #region NOT_IMPLEMENTED
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }


        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}