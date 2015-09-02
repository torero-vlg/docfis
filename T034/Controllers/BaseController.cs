using System.Web.Mvc;
using Db.DataAccess;
using Db.Entity.Administration;
using Ninject;
using T034.Tools.Auth;

namespace T034.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IBaseDb Db { get; set; }

        [Inject]
        public IAuthentication Auth { get; set; }
        public User CurrentUser
        {
            get
            {
                return ((IUserProvider)Auth.CurrentUser.Identity).User;
            }
        }
    }
}