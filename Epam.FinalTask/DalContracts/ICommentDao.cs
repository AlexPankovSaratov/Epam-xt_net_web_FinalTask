using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalContracts
{
    public interface ICommentDao
    {
        IEnumerable<Comment> GetAllCommentPhoto(int PhotoID);
        bool AddComment(Comment comment);
        bool RemoveComment(int ID);
    }
}
