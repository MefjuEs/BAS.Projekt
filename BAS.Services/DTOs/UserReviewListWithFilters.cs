﻿using System.Collections.Generic;

namespace BAS.AppServices
{
    public class UserReviewListWithFilters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int AllPages { get; set; }
        public List<UserReviewInListDTO> ReviewList { get; set; }
    }
}
