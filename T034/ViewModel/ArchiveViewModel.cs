using System.Collections.Generic;
using Db.Entity.Vgiik;

namespace T034.ViewModel
{
    public class ArchiveViewModel
    {
        public List<Person> Persons { get; set; }
        public List<DepartmentViewModel> Departments { get; set; }
    }
}