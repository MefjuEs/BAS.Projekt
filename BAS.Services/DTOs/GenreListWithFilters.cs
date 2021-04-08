using System.Collections.Generic;

namespace BAS.AppServices
{
    public class GenreListWithFilters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<GenreInListDTO> GenreList { get; set; }
    }
}
