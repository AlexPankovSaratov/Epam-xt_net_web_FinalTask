using DalContracts;
using Entities;
using ErrorProcessing;
using Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicContracts
{
    public class CommentLogic : ICommentLogic
    {
        private readonly ICommentDao _commentDao;
        public CommentLogic(ICommentDao commentDao)
        {
            _commentDao = commentDao;
        }

        public bool AddComment(int PhotoID, string Author, string TextComment)
        {
            try
            {
                if (PhotoID == 0 || Author == null || TextComment == null) return false;
                Comment comment = new Comment {PhotoID = PhotoID, Author = Author, TextComment = TextComment};
                return _commentDao.AddComment(comment);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return false;
            }
        }

        public IEnumerable<Comment> GetAllCommentPhoto(int PhotoID)
        {
            foreach (Comment item in _commentDao.GetAllCommentPhoto(PhotoID))
            {
                yield return item;
            }
        }

        public bool RemoveComment(int ID)
        {
            try
            {
                if (ID == 0) return false;
                return _commentDao.RemoveComment(ID);
            }
            catch (Exception ex)
            {
                MyLogger.AddLog(ex.Message, ex.StackTrace);
                Logger.Log.Error(ex.Message);
                return false;
            }
        }
    }
}
