using DalContracts;
using Logic;
using LogicContracts;
using SqlDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loc
{
    public static class DependencyResolver
    {
        private static IUserDao _userDao;
        public static IUserDao UserDao => _userDao ?? (_userDao = new SqlUserDao());
        private static IUserLogic _userLogic;
        public static IUserLogic UserLogic => _userLogic ?? (_userLogic = new UserLogic(UserDao));

        private static IPhotoDao _photoDao;
        public static IPhotoDao PhotoDao => _photoDao ?? (_photoDao = new SqlPhotoDao());
        private static IPhotoLogic _photoLogic;
        public static IPhotoLogic PhotoLogic => _photoLogic ?? (_photoLogic = new PhotoLogic(PhotoDao));
    }
}
