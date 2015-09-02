using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Mvc;

namespace T034.ViewModel
{
    public class DepartmentViewModel
    {
        public DepartmentViewModel()
        {
            Albums = new List<CarouselViewModel>();
            Files = new List<string>();
            Videos = new List<string>();
        }

        public int Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [Obsolete("Используется только в презентации, когда будет переделана презентация на использование инфы по подразделениям из БД это свойство можно удалить")]
        public List<string> Staff { get; set; }

        [AllowHtml]
        [DisplayName("Описание")]
        public string Text { get; set; }

        [DisplayName("Главное фото")]
        public string MainPhoto { get; set; }

        [AllowHtml]
        [DisplayName("Подпись под главным фото")]
        public string MainPhotoDescription { get; set; }

        public List<CarouselViewModel> Albums { get; set; }
		
		public IEnumerable<string> Files { get; set; }
		public string FilesFolder
        {
            get { return string.Format("/Content/images/department/{0}/files/", Id); }
        }

        [DisplayName("Публикации")]
        public string Nodes { get; set; }
        
        [DisplayName("Альбомы")]
        public string AlbumsIds { get; set; }

        public IEnumerable<string> Videos { get; set; }
    }
}