using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Db.DataAccess;
using Db.Entity;
using Db.Entity.Vgiik;
using T034.Tools.Attribute;
using T034.ViewModel;

namespace T034.Controllers
{
    public class DepartmentController : BaseController
    {
        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Department>();

                var model = new List<DepartmentViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        [HttpGet]
        [Role("Администратор")]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new DepartmentViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Department>(id.Value);
                model = Mapper.Map(item, model);
            }

            return View(model);
        }

        [Role("Администратор")]
        public ActionResult AddOrEdit(DepartmentViewModel model)
        {
            var item = new Department();
            if (model.Id > 0)
            {
                item = Db.Get<Department>(model.Id);
            }
            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

        public ActionResult Index(int departmentid)
        {
            var model = GetDepartment(departmentid);

            if (HttpContext.Request.IsAjaxRequest())
            {
                return PartialView("DepartmentPartialView", model);
            }
            return View(model);
        }

        public ActionResult Preview(int departmentid)
        {
            var model = GetDepartment(departmentid);
            return PartialView("Department/DepartmentPreview", model);
        }

        private DepartmentViewModel GetDepartment(int departmentid)
        {
            var item = Db.Get<Department>(departmentid);

            var model = new DepartmentViewModel();

            Mapper.Map(item, model);

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
            return model;
        }
    }
}
