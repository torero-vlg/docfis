using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {

            return RedirectToAction("Index", "Page", new {id = 1});
        }



        public void UploadNow(HttpPostedFileWrapper upload)
        {
            if (upload != null)
            {
                var imageName = upload.FileName;
                var path = System.IO.Path.Combine(Server.MapPath("~/Upload/Images"), imageName);
                upload.SaveAs(path);
            }
        }

        public ActionResult UploadPartial()
        {
            var appData = Server.MapPath("~/Upload/Images");
            var images = Directory.GetFiles(appData).Select(x => new ImageViewModel
            {
                Url = Url.Content("/Upload/Images/" + Path.GetFileName(x))
            });
            return View(images);
        }
    }
}
