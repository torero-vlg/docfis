using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Db.Entity;
using Db.Tools;
using T034.ViewModel.Common;

namespace T034.ViewModel
{
    public class CarouselViewModel
    {
        /// <summary>
        /// Создание карусели с описанием файлов
        /// </summary>
        /// <param name="nodes">Список фото</param>
        /// <param name="header">Заголовок карусели</param>
        /// <param name="interval">Интервал листания</param>
        public CarouselViewModel(IEnumerable<NodeViewModel> nodes, string header, string interval = "5000")
        {
            Header = header;
            Files = nodes;
            Interval = interval;

            CarouselId = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Создание карусели из папки с фото
        /// </summary>
        /// <param name="folder">Папка с фото</param>
        /// <param name="path">Путь</param>
        /// <param name="header">Заголовок карусели</param>
        /// <param name="interval">Интервал листания</param>
        public CarouselViewModel(string folder, string path, string header, string interval = "5000")
        {
            var directory = new DirectoryInfo(path);
            IEnumerable<string> files;
            try
            {
                files = directory.GetFiles().Select(f => f.Name);
            }
            catch (Exception ex)
            {
                MonitorLog.WriteLog("Ошибка выполнения CarouselViewModel : " + ex.Message, MonitorLog.typelog.Error, true);
                files = new string[1];
            }
            Header = header;
            Files = files.Select(f => new NodeViewModel{Path = folder + f, Description = ""});
            Folder = folder;
            Interval = interval;

            CarouselId = Guid.NewGuid().ToString();
        }

        public string CarouselId { get; private set; }
            

        public string Folder { get; set; }
        
        public string Header { get; set; }
        
        public string Interval { get; set; }

        public IEnumerable<NodeViewModel> Files { get; set; }
    }
}