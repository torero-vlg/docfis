using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Db.Entity.Vgiik;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class ArchiveController : BaseController
    {
        public ActionResult Person(int personId)
        {
            var model = GetPerson(personId);
            if (model == null)
            {
                return View("../404.cshtml");
            }

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("Archive/Person", model);
            }
            return View(model);
        }

        private PersonViewModel GetPerson(int personId)
        {
            var person = Db.Get<Person>(personId);

            PersonViewModel model = null;
            if (person != null)
            {
                model = new PersonViewModel
                    {
                        FullName =
                            string.Format("{0} - {1}", person.FullName, person.Title),
                        Docs = new List<CarouselViewModel>(),
                        PersonId = person.Id
                    };
                //foreach (var album in person.Albums)
                //{
                //    var nodes = album.Nodes.Select(n => new NodeViewModel { Description = n.Description, Path = n.Path });
                //    model.Docs.Add(new CarouselViewModel(nodes, album.Name, ""));
                //}
                model.Docs.AddRange(
                    person.Albums.Select(a => new CarouselViewModel(a.Path, Server.MapPath(a.Path), a.Name, "")));

                IEnumerable<string> files = new List<string>();

                try
                {
                    var directory = new DirectoryInfo(Server.MapPath(model.FilesFolder));
                    files = directory.GetFiles().Select(f => f.Name);
                }
                catch (Exception ex)
                {
                }

                model.Files = files;
            }

            if(personId == 24)
                model.Videos = new List<VideoViewModel> { new VideoViewModel { Width = "420", Height = "315", Source = "https://www.youtube.com/embed/c_obyoeuPGo" } };

            return model;
        }

        public ActionResult Department(int departmentid)
        {
            return RedirectToAction("Index", "Department", new {departmentid});
        }
    }
}
