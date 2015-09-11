using System;
using System.Collections.Generic;
using System.Web.Mvc;
using AutoMapper;
using Db.Entity;
using Db.Entity.Directory;
using T034.Tools.Attribute;
using T034.ViewModel.Common;

namespace T034.Controllers
{
    public class NodeController : BaseController
    {
        public NodeController()
        {

            Mapper.CreateMap<Node, NodeViewModel>()
                .ForMember(dest => dest.NodeTypeId, opt => opt.MapFrom(src => (int)src.NodeType));
            Mapper.CreateMap<NodeViewModel, Node>()
                .ForMember(dest => dest.NodeType, opt => opt.MapFrom(src => (NodeType)src.NodeTypeId));
               // .ForMember(dest => dest.Path, opt => opt.MapFrom(src => Sanitizer.GetSafeHtmlFragment(src.Path)));
        }

        [HttpGet]
        public ActionResult AddOrEdit(int? id)
        {
            var model = new NodeViewModel();
            if (id.HasValue)
            {
                var item = Db.Get<Node>(id.Value);
                model = Mapper.Map(item, model);
            }

            model.Types = new List<SelectListItem>
                {
                    new SelectListItem{Text = "Фото", Value = "1"},
                    new SelectListItem{Text = "Документ", Value = "2"},
                    new SelectListItem{Text = "Видео", Value = "3"}
                };

            return View(model);
        }

        [Role("Administrator")]
        public ActionResult AddOrEdit(NodeViewModel model)
        {
            var item = new Node();


            item = Mapper.Map(model, item);

            var result = Db.SaveOrUpdate(item);

            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            try
            {
                var items = Db.Select<Node>();

                var model = new List<NodeViewModel>();
                model = Mapper.Map(items, model);

                return View(model);
            }
            catch (Exception ex)
            {
                return View("ServerError", (object)"Получение списка публикаций");
            }
        }

    }
}
