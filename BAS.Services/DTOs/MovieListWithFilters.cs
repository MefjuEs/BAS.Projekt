using System.Collections.Generic;

namespace BAS.AppServices
{
    public class MovieListWithFilters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<MovieInListDTO> MovieList { get; set; }
    }
}
