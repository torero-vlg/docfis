using System.Collections.Generic;
using System.Web.Mvc;
using Db.Entity.Directory;

namespace T034.ViewModel.Common
{
    public class NodeViewModel
    {
        public int Id { get; set; }

        /// <summary>
        /// Путь к файлу, url и тд
        /// </summary>
        [AllowHtml]
        public string Path { get; set; }

        /// <summary>
        /// Описание, название для пользователя
        /// </summary>
        public string Description { get; set; }

        public int NodeTypeId { get; set; }

        public List<SelectListItem> Types { get; set; }

    }
}