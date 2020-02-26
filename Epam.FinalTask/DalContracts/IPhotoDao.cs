using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface IPhotoDao
    {
        Photo GetPhotoById(int PhotoId);
        bool AddPhoto(Photo photo);
        IEnumerable<Photo> GetAllPhoto();
        bool RemovePhoto(int PhotoId);
        bool LikePhoto(int PhotoId, int LikedUserId);
        bool RemoveLikePhoto(int PhotoId, int LikedUserId);
    }
}
