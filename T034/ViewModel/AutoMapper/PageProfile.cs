using System.Web;
using AutoMapper;
using Db.Entity;

namespace T034.ViewModel.AutoMapper
{
    public class PageProfile : Profile
    {
        private readonly HttpServerUtility _server;

        public PageProfile(HttpServerUtility server)
        {
            _server = server;
        }

        protected override void Configure()
        {
            Mapper.CreateMap<Page, PageViewModel>()
                  .ForMember(dest => dest.Content, opt => opt.MapFrom(src => HttpUtility.HtmlDecode(src.Content)));

            Mapper.CreateMap<PageViewModel, Page>()
                 // .ForMember(dest => dest.Content, opt => opt.MapFrom(src => Sanitizer.GetSafeHtmlFragment(src.Content)));
                  .ForMember(dest => dest.Content, opt => opt.MapFrom(src => _server.HtmlEncode(src.Content)));
        }
    }
}