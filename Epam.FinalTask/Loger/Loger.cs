using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorProcessing
{
    public class Loger
    {
        private static string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
        public static void AddLog(string Message, string StackTrace)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.AddLog";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var MessageParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Message",
                    Value = Message,
                };
                var StackTraceParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@StackTrace",
                    Value = StackTrace,
                };
                cmd.Parameters.Add(MessageParam);
                cmd.Parameters.Add(StackTraceParam);

                cmd.ExecuteNonQuery();
            }
        }
        public static IEnumerable<LogError> GetAllLog()
        {
            LinkedList<LogError> AllError = new LinkedList<LogError>();

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.GetAllLog";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    LogError logError = new LogError {Id = (int)sqlDataReader["Id"], Message = (string)sqlDataReader["Message"], StackTrace = (string)sqlDataReader["StackTrace"] };
                    AllError.AddLast(logError);
                }
            }
                return AllError;
        }
    }
}
