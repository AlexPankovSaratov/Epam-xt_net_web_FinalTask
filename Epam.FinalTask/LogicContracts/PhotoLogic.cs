using DalContracts;
using Entities;
using Logic;
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
            if (Title == null || Country == null || Image == null || AuthorId == 0) return false;
            Photo photo = new Photo { AuthorId = AuthorId, Country = Country, Image = Image, Title = Title, LikeUsersList = new HashSet<int>() };
            try
            {
                return _photoDao.AddPhoto(photo);
            }
            catch (Exception)
            {
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
            return _photoDao.GetPhotoById(PhotoId);
        }

        public bool LikePhoto(int PhotoId, int LikedUserId)
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

        public bool RemovePhoto(int PhotoId)
        {
            return _photoDao.RemovePhoto(PhotoId);
        }
    }
}
