using System;
using System.Web;
using System.Web.Security;
using T034.Models;

namespace T034.Tools.Auth
{
    public static class YandexAuth
    {
        public const string ClientId = "167f19d9d90a49939b70db7d17de7ad4";
        public const string Password = "c1cb7580b8a54e08b07038fd2e9ecd03";

        public const string InfoUrl = "https://login.yandex.ru/info";

        public static HttpCookie GetAuthorizationCookie(string code)
        {
            //var code = request.QueryString["code"];

            var stream = HttpTools.PostStream("https://oauth.yandex.ru/token",
                string.Format("grant_type=authorization_code&code={0}&client_id={1}&client_secret={2}", code, ClientId, Password));

            var model = SerializeTools.Deserialize<TokenModel>(stream);

            var userCookie = new HttpCookie("yandex_token")
            {
                Value = model.access_token,
                Expires = DateTime.Now.AddDays(30)
            };


            return userCookie;
        }

        public static UserModel GetUser(HttpRequestBase request)
        {
            var model = new UserModel{IsAutharization = false};
            try
            {
                var userCookie = request.Cookies["yandex_token"];
                if (userCookie != null)
                {
                    var stream = HttpTools.PostStream(InfoUrl, string.Format("oauth_token={0}", userCookie.Value));
                    model = SerializeTools.Deserialize<UserModel>(stream);
                    model.IsAutharization = true;
                }
            }
            catch (Exception)
            {
                //MonitorLog.WriteLog(ex.InnerException + ex.Message, MonitorLog.typelog.Error, true);
                model.IsAutharization = false;
            }

            return model;
        }
    }
}