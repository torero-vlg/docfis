using System.Collections.Generic;
using T034.ViewModel.Common;

namespace T034.ViewModel
{
    public class PersonViewModel
    {
        public PersonViewModel()
        {
            Videos = new List<VideoViewModel>();
        }

        public string FullName { get; set; }
        public int PersonId { get; set; }
        public List<CarouselViewModel> Docs { get; set; }

        public IEnumerable<string> Files { get; set; }
        public IEnumerable<VideoViewModel> Videos { get; set; }
    

        public string FilesFolder
        {
            get { return string.Format("/Content/images/people/{0}/files/", PersonId); }
        }
    }
}