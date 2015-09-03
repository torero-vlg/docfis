﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity;
using T034.ViewModel;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class PageController : BaseController
    {
        [HttpGet]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new PageViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Page>(id.Value);
                model = Mapper.Map(item, model);
            }

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult AddOrEdit(PageViewModel model)
        {
            var item = new Page();


            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Page>();

                var model = new List<PageViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка");
            }
        }

        public ActionResult Index(int id)
        {
            var model = new PageViewModel();

            var item = Db.Get<Page>(id);
            model = Mapper.Map(item, model);

            return View(model);
        }

    }
}
