using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using T034.Tools.Auth;

namespace T034.Controllers
{
    public class AuthController : BaseController
    {
        public ActionResult LoginWithYandex(string code)
        {
//            var userCookie = YandexAuth.GetAuthorizationCookie(Request);
            var accessToken = YandexAuth.GetAuthorizationCookie(Response.Cookies, code, Repository);

            FormsAuthentication.SetAuthCookie(accessToken, true);
            
            return RedirectToActionPermanent("Index", "Home");
        }

        public ActionResult Logout()
        {
            HttpCookie aCookie;
            string cookieName;
            int limit = Request.Cookies.Count;

            for (int i = 0; i < limit; i++)
            {
                cookieName = Request.Cookies[i].Name;
                aCookie = new HttpCookie(cookieName);
                aCookie.Value = "";
                Response.Cookies.Set(aCookie);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult RedirectToYandex()
        {
            return Redirect(string.Format("https://oauth.yandex.ru/authorize?response_type=code&client_id={0}", YandexAuth.ClientId)); ;
        }
    }
}
