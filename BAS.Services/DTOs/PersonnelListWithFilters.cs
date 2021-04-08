using BAS.Database;
using System.Collections.Generic;

namespace BAS.AppServices
{
    public class PersonnelListWithFilters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<Personnel> PersonnelList { get; set; }
    }
}
