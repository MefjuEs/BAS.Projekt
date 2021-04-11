using System.Collections.Generic;

namespace BAS.AppServices
{
    public class MovieReviewListWithFilters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<MovieReviewInListDTO> ReviewList { get; set; }
    }
}
