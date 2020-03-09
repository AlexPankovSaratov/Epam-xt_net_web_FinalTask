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
    public class SqlPhotoDao : IPhotoDao
    {
        private string conStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;

        public bool AddPhoto(Photo photo)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.AddPhoto";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var TitleParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Title",
                    Value = photo.Title,
                };
                var CountryParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Country",
                    Value = photo.Country,
                };
                var AuthorIdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@AuthorId",
                    Value = photo.AuthorId,
                };
                var ImageParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Image",
                    Value = Convert.ToBase64String(photo.Image),
                };
                cmd.Parameters.Add(TitleParam);
                cmd.Parameters.Add(CountryParam);
                cmd.Parameters.Add(AuthorIdParam);
                cmd.Parameters.Add(ImageParam);

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool EditPhoto(int photoId, string newTitle, string newCounry)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.EditPhoto";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var TitleParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Title",
                    Value = newTitle,
                };
                var CountryParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.String,
                    ParameterName = "@Country",
                    Value = newCounry,
                };
                var IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Id",
                    Value = photoId,
                };
                cmd.Parameters.Add(TitleParam);
                cmd.Parameters.Add(CountryParam);
                cmd.Parameters.Add(IdParam);

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public IEnumerable<Photo> GetAllPhoto()
        {
            try
            {
                HashSet<Photo> AllPhoto = new HashSet<Photo>();
                using (SqlConnection con = new SqlConnection(conStr))
                {
                    SqlCommand cmd = con.CreateCommand();
                    con.Open();
                    cmd.CommandText = "dbo.GetAllPhoto";
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlDataReader sqlDataReader = cmd.ExecuteReader();
                    User[] usersAttachment = new User[] { };
                    while (sqlDataReader.Read())
                    {
                        int Id = (int)sqlDataReader["Id"];
                        string Title = (string)sqlDataReader["Title"];
                        string Country = (string)sqlDataReader["Country"];
                        int AuthorId = (int)sqlDataReader["AuthorId"];
                        byte[] Image = Convert.FromBase64String((string)sqlDataReader["Image"]);
                        int LikeAuthorId = 0;
                        HashSet<int> LikeUsersList = new HashSet<int> { };
                        if (!System.DBNull.Value.Equals(sqlDataReader["User_Id"]))
                        {
                            LikeAuthorId = (int)sqlDataReader["User_Id"];
                            LikeUsersList.Add(LikeAuthorId);
                        }
                        Photo photo = new Photo { Id = Id, Title = Title, Country = Country, AuthorId = AuthorId, LikeUsersList = LikeUsersList, Image = Image };
                        Photo SerchPhoto = AllPhoto.FirstOrDefault(element => element.Id == photo.Id);
                        if (SerchPhoto != null && LikeAuthorId != 0) SerchPhoto.LikeUsersList.Add(LikeAuthorId);
                        else AllPhoto.Add(photo);
                    }
                }
                return AllPhoto;
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return null;
            } 
        }

        public Photo GetPhotoById(int PhotoId)
        {
            Photo photo = null;
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.GetPhotoById";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Id",
                    Value = PhotoId,
                };
                cmd.Parameters.Add(IdParam);
                SqlDataReader sqlDataReader = cmd.ExecuteReader();
                HashSet<int> LikeUsersList = new HashSet<int> { };
                int LikeAuthorId = 0;
                while (sqlDataReader.Read())
                {
                    if (!System.DBNull.Value.Equals(sqlDataReader["User_Id"]))
                    {
                        LikeAuthorId = (int)sqlDataReader["User_Id"];
                        LikeUsersList.Add(LikeAuthorId);
                    }
                    if (photo == null)
                    {
                        photo = new Photo
                        {
                            Id = (int)sqlDataReader["Id"],
                            Country = (string)sqlDataReader["Country"],
                            Title = (string)sqlDataReader["Title"],
                            AuthorId = (int)sqlDataReader["AuthorId"],
                            Image = Convert.FromBase64String((string)sqlDataReader["Image"])
                        };
                    }
                }
                photo.LikeUsersList = LikeUsersList;
            }
            return photo;
        }

        public bool LikePhoto(int PhotoId, int LikedUserId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.LikePhoto";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var PhotoIdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Photo_Id",
                    Value = PhotoId,
                };
                var LikedUserIdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@User_Id",
                    Value = LikedUserId,
                };
                cmd.Parameters.Add(PhotoIdParam);
                cmd.Parameters.Add(LikedUserIdParam);

                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool RemoveLikePhoto(int PhotoId, int LikedUserId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.RemoveLikePhoto";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Photo_Id",
                    Value = PhotoId,
                };
                var LikedUserIdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@User_Id",
                    Value = LikedUserId,
                };
                cmd.Parameters.Add(IdParam);
                cmd.Parameters.Add(LikedUserIdParam);
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public bool RemovePhoto(int PhotoId)
        {
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = con.CreateCommand();
                con.Open();
                cmd.CommandText = "dbo.RemovePhoto";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                var IdParam = new SqlParameter()
                {
                    DbType = System.Data.DbType.Int32,
                    ParameterName = "@Id",
                    Value = PhotoId,
                };
                cmd.Parameters.Add(IdParam);
                cmd.ExecuteNonQuery();
            }
            return true;
        }
    }
}
