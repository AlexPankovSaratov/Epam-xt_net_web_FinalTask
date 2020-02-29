using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface IPhotoLogic
    {
        Photo GetPhotoById(int PhotoId);
        bool AddPhoto(string Title, string Country, byte[] Image, int AuthorId);
        IEnumerable<Photo> GetAllPhoto();
        bool RemovePhoto(int PhotoId);
        bool LikePhoto(int PhotoId, int LikedUserId);
    }
}
