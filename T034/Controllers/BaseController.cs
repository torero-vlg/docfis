using System.Web;
using System.Web.Mvc;
using Db.DataAccess;
using Db.Entity.Administration;
using Ninject;
using T034.Repository;
using T034.Tools.Auth;

namespace T034.Controllers
{
    public class BaseController : Controller
    {
        [Inject]
        public IBaseDb Db { get; set; }

        [Inject]
        public IRepository Repository { get; set; }
    }
}