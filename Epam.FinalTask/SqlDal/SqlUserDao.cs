using DalContracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDal
{
    public class SqlUserDao : IUserDao
    {
        private string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public bool AddNewUser(User NewUser)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.AddNewUser";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var LoginParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Login",
                    Value = NewUser.Login,
                };
                var PasswordParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Password",
                    Value = NewUser.Password,
                };
                cmd.Parameters.Add(LoginParam);
                cmd.Parameters.Add(PasswordParam);

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool AddUserRole(int UserID, string RoleName)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.AddUserRole";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var User_IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@User_Id",
                    Value = UserID,
                };
                var RoleNameParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@RoleName",
                    Value = RoleName,
                };
                cmd.Parameters.Add(User_IdParam);
                cmd.Parameters.Add(RoleNameParam);

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            HashSet<User> AllUsers = new HashSet<User>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.GetAllUsers";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                User[] usersAttachment = new User[] { };
                while (sqlDataReader.Read())
                {
                    int Id = (int)sqlDataReader["Id"];
                    string Login = (string)sqlDataReader["Login"];
                    string Password = (string)sqlDataReader["Password"];
                    string Role = "";
                    HashSet<string> Roles = new HashSet<string> { };
                    if (!System.DBNull.Value.Equals(sqlDataReader["RoleName"]))
                    {
                        Role = (string)sqlDataReader["RoleName"];
                        Roles.Add(Role);
                    }
                    User user = new User { Id = Id, Login = Login, Password = Password, Roles = Roles };
                    User SerchUser = AllUsers.FirstOrDefault(element => element.Login == user.Login);
                    if (SerchUser != null && Role != "") SerchUser.Roles.Add(Role);
                    else AllUsers.Add(user);
                }
            }
            return AllUsers;
        }

        public User GetUserById(int UserID)
        {
            User user = null;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.GetUserById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Id",
                    Value = UserID,
                };
                cmd.Parameters.Add(IdParam);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                HashSet<string> Roles = new HashSet<string> { };
                string RoleName = "";
                while (sqlDataReader.Read())
                {
                    if (!System.DBNull.Value.Equals(sqlDataReader["RoleName"]))
                    {
                        RoleName = (string)sqlDataReader["RoleName"];
                        Roles.Add(RoleName);
                    }
                    if (user == null)
                    {
                        user = new User { Id = (int)sqlDataReader["Id"], Login = (string)sqlDataReader["Login"], Password = (string)sqlDataReader["Password"] };
                    }
                }
                user.Roles = Roles;
            }
            return user;
        }
    }
}
