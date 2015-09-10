﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity.Administration;
using T034.ViewModel;

namespace T034.Controllers
{
    public class RoleController : BaseController
    {
        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Role>();

                var model = new List<RoleViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        [HttpGet]
        //[Authorize]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new RoleViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Role>(id.Value);
                model = Mapper.Map(item, model);
            }

            return View(model);
        }

       // [Authorize]
        public ActionResult AddOrEdit(RoleViewModel model)
        {
            var item = new Role();
            if (model.Id > 0)
            {
                item = Db.Get<Role>(model.Id);
            }
            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

        public ActionResult Index(int id)
        {
            var model = new RoleViewModel();

            var item = Db.Get<Role>(id);
            if (item == null)
            {
                return View("ServerError", (object)"Страница не найдена");
            }
            model = Mapper.Map(item, model);

            return View(model);
        }
    }
}
