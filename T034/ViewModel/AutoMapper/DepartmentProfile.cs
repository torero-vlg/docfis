using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Db.Entity;
using Db.Entity.Directory;
using Db.Entity.Vgiik;

namespace T034.ViewModel.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        private readonly HttpServerUtility _server;

        public DepartmentProfile(HttpServerUtility server)
        {
            _server = server;
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Department, DepartmentViewModel>()
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => src.Albums.Select(a => new CarouselViewModel(a.Path, _server.MapPath(a.Path), a.Name, ""))))
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => IdsToString(src.Nodes)))
                .ForMember(dest => dest.AlbumsIds, opt => opt.MapFrom(src => IdsToString(src.Albums)))
                .ForMember(dest => dest.Videos, opt => opt.MapFrom(src => src.Nodes.Where(n => n.NodeType == NodeType.Video).Select(n => n.Path)));

            Mapper.CreateMap<DepartmentViewModel, Department>()
                .ForMember(dest => dest.Nodes, opt => opt.MapFrom(src => StringToCollection<Node>(src.Nodes)))
                .ForMember(dest => dest.Albums, opt => opt.MapFrom(src => StringToCollection<Album>(src.AlbumsIds)));
        }

        private static List<T> StringToCollection<T>(string ids) where T : Entity, new()
        {
            return string.IsNullOrEmpty(ids) ? null : new List<T>(ids.Split(new string[] { "," }, StringSplitOptions.None).Select(n => new T { Id = Convert.ToInt32(n) }));
        }

        private static string IdsToString<T>(ICollection<T> collection) where T : Entity
        {
            return collection == null || !collection.Any() ? "" : collection.Select(n => n.Id.ToString()).Aggregate((i, j) => i.ToString() + "," + j.ToString());
        }
    }
}