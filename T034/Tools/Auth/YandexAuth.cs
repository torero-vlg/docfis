using System;
using System.Web;
using System.Web.Security;
using T034.Models;

namespace T034.Tools.Auth
{
    public static class YandexAuth
    {
        public const string ClientId = "df88eaabdc3c4090bd638f60a19c1d1b";
        public const string Password = "09700accd8c5418f9953347b617c63ba";

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