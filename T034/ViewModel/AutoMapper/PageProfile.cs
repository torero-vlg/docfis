using System.Text;
using System.Web;
using AutoMapper;
using Db.Entity;
using Microsoft.Security.Application;

namespace T034.ViewModel.AutoMapper
{
    public class PageProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<Page, PageViewModel>()
                  .ForMember(dest => dest.Content, opt => opt.MapFrom(src => HttpUtility.HtmlDecode(src.Content)));

            Mapper.CreateMap<PageViewModel, Page>()
                  //.ForMember(dest => dest.Content, opt => opt.MapFrom(src => Sanitizer.GetSafeHtmlFragment(src.Content)));
                  .ForMember(dest => dest.Content, opt => opt.MapFrom(src => GetSafeHtml(src.Content)));
        }


        private string GetSafeHtml(string htmlInputTxt)
        {
            var sb = new StringBuilder(
                           HttpUtility.HtmlEncode(htmlInputTxt));

            sb.Replace("&lt;script&gt;", "");
            sb.Replace("&lt;/script&gt;", "");
            return sb.ToString();
        }
    }
}