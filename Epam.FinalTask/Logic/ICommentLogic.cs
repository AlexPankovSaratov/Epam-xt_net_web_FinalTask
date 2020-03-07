using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public interface ICommentLogic
    {
        IEnumerable<Comment> GetAllCommentPhoto(int PhotoID);
        bool AddComment(int PhotoID, string Author, string TextComment);
        bool RemoveComment(int ID);
    }
}
