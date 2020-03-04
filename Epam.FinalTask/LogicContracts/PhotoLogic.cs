using DalContracts;
using Entities;
using Logic;
using ErrorProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicContracts
{
    public class PhotoLogic : IPhotoLogic
    {
        private readonly IPhotoDao _photoDao;
        public PhotoLogic(IPhotoDao photoDao)
        {
            _photoDao = photoDao;
        }
        public bool AddPhoto(string Title, string Country, byte[] Image, int AuthorId)
        {   
            try
            {
                if (Title == null || Country == null || Image == null || AuthorId == 0) return false;
                Photo photo = new Photo { AuthorId = AuthorId, Country = Country, Image = Image, Title = Title, LikeUsersList = new HashSet<int>() };
                return _photoDao.AddPhoto(photo);
            }
            catch (Exception ex)
            {
                Loger.AddLog(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public bool EditPhoto(int PhotoId, string NewTitle, string NewCounry)
        {
            try
            {
                if (PhotoId == 0 || (NewTitle == "" && NewCounry == "")) return false;
                if(NewTitle == "")
                {
                    NewTitle = _photoDao.GetPhotoById(PhotoId).Title;
                }
                if (NewCounry == "")
                {
                    NewCounry = _photoDao.GetPhotoById(PhotoId).Country;
                }
                return _photoDao.EditPhoto(PhotoId, NewTitle, NewCounry);
            }
            catch (Exception ex)
            {
                Loger.AddLog(ex.Message, ex.StackTrace);
                return false;
            }
        }

        public IEnumerable<Photo> GetAllPhoto()
        {
            foreach (Photo item in _photoDao.GetAllPhoto())
            {
                yield return item;
            }
        }

        public Photo GetPhotoById(int PhotoId)
        {
            try
            {
                if (PhotoId == 0) return null;
                return _photoDao.GetPhotoById(PhotoId);
            }
            catch (Exception ex)
            {
                Loger.AddLog(ex.Message, ex.StackTrace);
                return null;
            } 
        }

        public bool LikePhoto(int PhotoId, int LikedUserId)
        {
            try
            {
                Photo photo = GetPhotoById(PhotoId);
                bool UserInArray = false;
                foreach (int item in photo.LikeUsersList)
                {
                    if (item == LikedUserId) UserInArray = true;
                }
                if (!UserInArray) return _photoDao.LikePhoto(PhotoId, LikedUserId);
                else return _photoDao.RemoveLikePhoto(PhotoId, LikedUserId);
            }
            catch (Exception ex)
            {
                Loger.AddLog(ex.Message, ex.StackTrace);
                return false;
            }  
        }

        public bool RemovePhoto(int PhotoId)
        {
            try
            {
                return _photoDao.RemovePhoto(PhotoId);
            }
            catch (Exception ex)
            {
                Loger.AddLog(ex.Message, ex.StackTrace);
                return false;
            }
        }
    }
}
