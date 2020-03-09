using DalContracts;
using Entities;
using ErrorProcessing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlDal
{
    public class SqlCommentDao : ICommentDao
    {
        private string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public bool AddComment(Comment comment)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.AddComment";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var TextCommentParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@TextComment",
                    Value = comment.TextComment,
                };
                var AuthorParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Author",
                    Value = comment.Author,
                };
                var PhotoIDParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@PhotoID",
                    Value = comment.PhotoID,
                };
                cmd.Parameters.Add(TextCommentParam);
                cmd.Parameters.Add(AuthorParam);
                cmd.Parameters.Add(PhotoIDParam);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public IEnumerable<Comment> GetAllCommentPhoto(int PhotoID)
        {
            try
            {
                LinkedList<Comment> AllComment = new LinkedList<Comment>();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "dbo.GetAllCommentPhoto";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        int Id = (int)sqlDataReader["Id"];
                        string Author = (string)sqlDataReader["Author"];
                        int IDPhoto = (int)sqlDataReader["PhotoID"];
                        string TextComment = (string)sqlDataReader["TextComment"];
                        if(IDPhoto == PhotoID)
                        {
                            AllComment.AddLast(new Comment { ID = Id, Author = Author, PhotoID = PhotoID, TextComment = TextComment });
                        }
                    }
                }
                return AllComment;
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return null;
            }
        }

        public bool RemoveComment(int ID)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.RemoveComment";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var IDParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@ID",
                    Value = ID,
                };
                cmd.Parameters.Add(IDParam);
                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}
